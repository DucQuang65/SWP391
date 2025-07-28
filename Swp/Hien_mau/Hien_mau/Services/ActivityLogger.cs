using Hien_mau.Data;
using Hien_mau.Models;
using Microsoft.EntityFrameworkCore;

namespace Hien_mau.Services
{
    public class ActivityLogger
    {
        private readonly Hien_mauContext _context;

        public ActivityLogger(Hien_mauContext context)
        {
            _context = context;
        }

        public async Task LogAsync(int userId, string action, string entityType, int? entityId = null, string? description = null)
        {
            if (!await _context.Users.AnyAsync(u => u.UserId == userId))
            {
                throw new ArgumentException($"UserId {userId} does not exist.");
            }

            var log = new ActivityLogs
            {
                UserId = userId,
                ActivityType = action,
                EntityType = entityType,
                EntityId = entityId,
                Description = description,
                CreatedAt = DateTime.Now,
            };

            _context.ActivityLogs.Add(log);
            await _context.SaveChangesAsync();
        }
    }
}