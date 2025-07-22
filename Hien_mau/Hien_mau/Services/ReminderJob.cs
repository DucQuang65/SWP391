using Hien_mau.Data;
using Hien_mau.Models;
using Microsoft.EntityFrameworkCore;

public class ReminderJob
{
    private readonly Hien_mauContext _context;

    public ReminderJob(Hien_mauContext context)
    {
        _context = context;
    }

    public async Task Run()
    {
        var now = DateTime.Now;

        var reminders = await _context.Reminders
            .Where(r => !r.IsDisabled && !r.IsSent && r.RemindAt <= now)
            .ToListAsync();

        foreach (var reminder in reminders)
        {
            Console.WriteLine($"[ReminderJob] Nhắc User {reminder.UserId}: {reminder.Message}");

            var notification = new Notifications
            {
                UserId = reminder.UserId,
                Title = reminder.Type == "Recovery" ? "Nhắc hồi phục hiến máu" : "Nhắc lịch hiến máu",
                Message = reminder.Message,
                Type = "Reminder",
                Priority = false,
                SentAt = DateTime.Now
            };

            _context.Notifications.Add(notification);

            reminder.IsSent = true;
            reminder.SentAt = DateTime.Now;
        }

        await _context.SaveChangesAsync();
    }
}
