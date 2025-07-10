using Hien_mau.Data;
using Hien_mau.Models;
using Microsoft.EntityFrameworkCore;

namespace Hien_mau.Services
{
    public class CancelExpiredService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<CancelExpiredService> _logger;

        public CancelExpiredService(IServiceScopeFactory scopeFactory, ILogger<CancelExpiredService> logger)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Checking and canceling expired blood units...");

                using (var scope = _scopeFactory.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<Hien_mauContext>();

                    var expiredInventories = await context.BloodInventories
                        .AsNoTracking()
                        .Where(i => i.ExpirationDate < DateTime.Now && i.Quantity > 0)
                        .ToListAsync();

                    foreach (var inventory in expiredInventories)
                    {
                        var updatedInventory = new BloodInventories
                        {
                            InventoryId = inventory.InventoryId,
                            BloodGroup = inventory.BloodGroup,
                            RhType = inventory.RhType,
                            ComponentId = inventory.ComponentId,
                            BagType = inventory.BagType,
                            Quantity = 0,
                            IsRare = inventory.IsRare,
                            Status = 0,
                            ReceivedDate = inventory.ReceivedDate,
                            LastUpdated = DateTime.Now,
                            ExpirationDate = inventory.ExpirationDate
                        };

                        context.BloodInventories.Update(updatedInventory);

                        var history = new BloodInventoryHistories
                        {
                            InventoryId = inventory.InventoryId,
                            BloodGroup = inventory.BloodGroup,
                            RhType = inventory.RhType,
                            ComponentId = inventory.ComponentId,
                            ActionType = "Hủy",
                            Quantity = inventory.Quantity,
                            BagType = inventory.BagType,
                            Notes = "Máu hết hạn tự động hủy",
                            PerformedBy = -1,
                            PerformedAt = DateTime.Now,
                            ReceivedDate = inventory.ReceivedDate,
                            ExpirationDate = inventory.ExpirationDate
                        };

                        context.BloodInventoryHistories.Add(history);
                        await context.SaveChangesAsync();
                    }

                    if (expiredInventories.Count > 0)
                    {
                        _logger.LogInformation($"Successfully canceled {expiredInventories.Count} expired blood unit");
                    }
                    else
                    {
                        _logger.LogInformation("No expired blood units found.");
                    }
                }


                await Task.Delay(TimeSpan.FromHours(24), stoppingToken);
            }
        }
    }
}
