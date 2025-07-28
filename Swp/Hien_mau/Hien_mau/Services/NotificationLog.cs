using Hien_mau.Data;
using Hien_mau.Models;
using Microsoft.EntityFrameworkCore;

namespace Hien_mau.Services
{
    public class NotificationLog
    {
        private readonly Hien_mauContext _context;
        public NotificationLog(Hien_mauContext context)
        {
            _context = context;
        }
        public async Task NotiLog(int userId, string title, string message, string type)
        {
            if (!await _context.Users.AnyAsync(u => u.UserId == userId))
            {
                throw new ArgumentException($"UserId {userId} does not exist.");
            }
            var notification = new Notifications
            {
                UserId = userId,
                Title = title,
                Message = message,
                Type = type,
                IsRead = false,
                SentAt = DateTime.Now
            };
            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();
        }
    }
}
