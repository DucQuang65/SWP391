using Hien_mau.Data;
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
        [HttpGet("admin")]
        public async Task<ActionResult<IEnumerable<object>>> GetAllLogs()
        {
            var logs = await _context.ActivityLogs
                .Include(l => l.User)
                .OrderByDescending(l => l.CreatedAt)
                .Take(100)
                .Select(l => new
                {
                    l.LogId,
                    l.UserId,
                    UserName = l.User.Name,
                    RoleName = l.User.Role.RoleName,
                    l.ActivityType,
                    l.EntityType,
                    l.EntityId,
                    l.Description,
                    l.CreatedAt
                }).ToListAsync();

            return Ok(logs);
        }

    }
}
