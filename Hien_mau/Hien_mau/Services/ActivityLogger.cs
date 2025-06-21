using Hien_mau.Data;
using Hien_mau.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Hien_mau.Services
{
    public class ActivityLogger
    {
        private readonly Hien_mauContext _context;

        public ActivityLogger(Hien_mauContext context)
        {
            _context = context;
        }

        public async Task LogAsync(int userId, string action, string entityType, int? entityId = null, string? oldValues = null, string? newValues = null, string? description = null)
        {
            if (!await _context.Users.AnyAsync(u => u.UserId == userId))
            {
                throw new ArgumentException($"UserId {userId} does not exist.");
            }

            var log = new ActivityLog
            {
                UserID = userId,
                EventType = action,
                EntityType = entityType,
                EntityId = entityId,
                OldValues = TruncateJson(oldValues, 4000),
                NewValues = TruncateJson(newValues, 4000),
                CreatedAt = DateTime.Now,
                Description = description
            };

            _context.ActivityLogs.Add(log);
            await _context.SaveChangesAsync();
        }

        private string TruncateJson(string json, int maxLength)
        {
            if (string.IsNullOrEmpty(json) || json.Length <= maxLength)
            {
                return json;
            }
            return json.Substring(0, maxLength - 3) + "...";
        }
    }
}