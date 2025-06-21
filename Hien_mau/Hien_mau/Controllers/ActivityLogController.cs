using Hien_mau.Data;
using Hien_mau.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hien_mau.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivityLogController : ControllerBase
    {
        private readonly Hien_mauContext _context;

        public ActivityLogController(Hien_mauContext context)
        {
            _context = context;
        }

        // GET: api/ActivityLogController
        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetAuditLogs()
        {
            var logs = await _context.ActivityLogs
                .Include(l => l.User)
                .OrderByDescending(l => l.CreatedAt)
                .Take(200) // limit to avoid overload
                .ToListAsync();

            var result = logs.Select(l => new
            {
                l.LogId,
                l.UserID,
                UserName = l.User.Name,
                l.EventType,
                l.EntityType,
                l.EntityId,
                l.OldValues,
                l.NewValues,
                l.CreatedAt
            });

            return Ok(result);
        }

        // GET: api/AuditLog/5
        [HttpGet("{id}")]
        public async Task<ActionResult<object>> GetAuditLog(int id)
        {
            var log = await _context.ActivityLogs
                .Include(l => l.User)
                .FirstOrDefaultAsync(l => l.LogId == id);

            if (log == null) return NotFound();

            return Ok(new
            {
                log.LogId,
                log.UserID,
                UserName = log.User.Name,
                log.EventType,
                log.EntityType,
                log.EntityId,
                log.OldValues,
                log.NewValues,
                log.CreatedAt
            });
        }

        // GET: api/AuditLog/by-user/3
        [HttpGet("by-user/{userId}")]
        public async Task<ActionResult<IEnumerable<object>>> GetAuditLogsByUser(int userId)
        {
            var logs = await _context.ActivityLogs
                .Include(l => l.User)
                .Where(l => l.UserID == userId)
                .OrderByDescending(l => l.CreatedAt)
                .ToListAsync();

            var result = logs.Select(l => new
            {
                l.LogId,
                l.UserID,
                UserName = l.User.Name,
                l.EventType,
                l.EntityType,
                l.EntityId,
                l.OldValues,
                l.NewValues,
                l.CreatedAt
            });

            return Ok(result);
        }
    }
}
