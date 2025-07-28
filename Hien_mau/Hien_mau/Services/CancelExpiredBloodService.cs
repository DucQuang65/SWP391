using Hien_mau.Data;
using Hien_mau.Models;
using Microsoft.EntityFrameworkCore;

public class BloodInventoryExpiryJob
{
    private readonly Hien_mauContext _context;

    public BloodInventoryExpiryJob(Hien_mauContext context)
    {
        _context = context;
    }

    public async Task CheckAndExpireBlood()
    {
        var now = DateTime.Now;

       
        var expiredCheckIns = await _context.BloodInventoryHistories
            .Where(h => h.ActionType == "CheckIn"
                        && h.ExpirationDate < now)
            .ToListAsync();

        int expiredCount = 0;

        foreach (var item in expiredCheckIns)
        {
           
            var alreadyCheckedOut = await _context.BloodInventoryHistories.AnyAsync(h =>
                h.ActionType == "CheckOut"
                && h.Notes == "Tự động hủy do hết hạn"
                && h.ReceivedDate == item.ReceivedDate
                && h.ExpirationDate == item.ExpirationDate
                && h.InventoryId == item.InventoryId);

            if (alreadyCheckedOut) continue;

           
            var expiredLog = new BloodInventoryHistories
            {
                InventoryId = item.InventoryId,
                ActionType = "Hủy",
                Quantity = item.Quantity,
                Notes = "Tự động hủy do hết hạn",
                PerformedBy = -1,
                PerformedAt = DateTime.Now,
                BagType = item.BagType,
                ReceivedDate = item.ReceivedDate,
                ExpirationDate = item.ExpirationDate
            };
            _context.BloodInventoryHistories.Add(expiredLog);

           
            var inventory = await _context.BloodInventories.FindAsync(item.InventoryId);
            if (inventory != null && inventory.Quantity >= item.Quantity)
            {
                inventory.Quantity -= item.Quantity;
            }

            expiredCount++;
        }

        await _context.SaveChangesAsync();

        Console.WriteLine($" Đã tự động hủy {expiredCount} túi máu hết hạn.");
    }
}
