using Audit.Core;
using Hien_mau.Models;
using Newtonsoft.Json;

namespace Hien_mau.Data
{
    public class ActivityLogDataProvider : AuditDataProvider
    {
        private readonly Hien_mauContext _context;

        public ActivityLogDataProvider(Hien_mauContext context)
        {
            _context = context;
        }

        public override object InsertEvent(AuditEvent auditEvent)
        {
            var log = CreateActivityLog(auditEvent);
            _context.ActivityLogs.Add(log);
            _context.SaveChanges();
            return log.LogId;
        }

        public override async Task<object> InsertEventAsync(AuditEvent auditEvent)
        {
            var log = CreateActivityLog(auditEvent);
            _context.ActivityLogs.Add(log);
            await _context.SaveChangesAsync();
            return log.LogId;
        }

        private ActivityLog CreateActivityLog(AuditEvent auditEvent)
        {
            var userId = int.TryParse(auditEvent.CustomFields.GetValueOrDefault("UserId")?.ToString(), out var parsedUserId) ? parsedUserId : 1;
            return new ActivityLog
            {
                UserID = userId,
                EventType = auditEvent.EventType ?? "Unknown",
                EntityType = auditEvent.CustomFields.GetValueOrDefault("EntityType")?.ToString() ?? "Unknown",
                EntityId = auditEvent.CustomFields.GetValueOrDefault("EntityId") is string entityStr && int.TryParse(entityStr, out var eid) ? eid : null,
                OldValues = auditEvent.CustomFields.GetValueOrDefault("OldValues")?.ToString(),
                NewValues = auditEvent.CustomFields.GetValueOrDefault("NewValues")?.ToString(),
                CreatedAt = DateTime.UtcNow
            };
        }
    }
}
