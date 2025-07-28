using Hien_mau.Data;
using Hien_mau.Models;
using Microsoft.EntityFrameworkCore;

public class AppointmentCompletionJob
{
    private readonly Hien_mauContext _context;

    public AppointmentCompletionJob(Hien_mauContext context)
    {
        _context = context;
    }

    public async Task Run()
    {
        var now = DateTime.Now;

        var appointments = await _context.Appointments
            .Where(a => (a.Process == 2 && a.Status == true) || a.Process == 3 || a.Process == 4)
            .Where(a => a.DonationDate.Value.AddDays(84).Date <= now.Date)
            .ToListAsync();

        foreach (var appointment in appointments)
        {
            var remindDate = appointment.DonationDate.Value.AddDays(84).Date;

            bool exists = await _context.Reminders.AnyAsync(r =>
                r.UserId == appointment.UserID &&
                r.Type == "Recovery" &&
                r.RemindAt.Date == remindDate);

            if (exists) continue;

            var reminder = new Reminder
            {
                UserId = appointment.UserID,
                Type = "Recovery",
                Message = "Đã đủ 12 tuần kể từ lần hiến máu trước! Bạn đã có thể hiến máu lại.",
                RemindAt = remindDate,
                CreatedAt = DateTime.Now,
                IsSent = false,
                IsDisabled = false
            };

            _context.Reminders.Add(reminder);
        }

        await _context.SaveChangesAsync();
    }
}