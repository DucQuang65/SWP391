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

       
        var donations = await _context.BloodDonationHistories
            .Include(d => d.Appointment)
            .Where(d => d.IsSuccess
                        && d.DonationDate.AddDays(84).Date <= now.Date)
            .ToListAsync();

        foreach (var donation in donations)
        {
            var remindDate = donation.DonationDate.AddDays(84).Date;

            bool exists = await _context.Reminders.AnyAsync(r =>
                r.UserId == donation.Appointment.UserId &&
                r.Type == "Recovery" &&
                r.RemindAt.Date == remindDate);

            if (exists) continue;

            var reminder = new Reminder
            {
                UserId = donation.Appointment.UserId,
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
