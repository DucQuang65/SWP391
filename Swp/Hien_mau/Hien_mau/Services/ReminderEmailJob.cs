using Cronos;
using Hien_mau.Interface;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Hien_mau.Services
{
    public class ReminderEmailJob : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly CronExpression _cron;
        private readonly TimeZoneInfo _timeZone;

        public ReminderEmailJob(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
            _cron = CronExpression.Parse("0 7 * * *"); 
            _timeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var next = _cron.GetNextOccurrence(DateTimeOffset.Now, _timeZone);
                if (next.HasValue)
                {
                    var delay = next.Value - DateTimeOffset.Now;
                    if (delay.TotalMilliseconds > 0)
                        await Task.Delay(delay, stoppingToken);
                }

                using var scope = _scopeFactory.CreateScope();
                var emailService = scope.ServiceProvider.GetRequiredService<ISendEmail>();
                await emailService.SendAppointmentReminders();
            }
        }
    }
}
