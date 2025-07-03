using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Hien_mau.Data;
using FirebaseAdmin.Messaging;
using Hien_mau.Models;
using Microsoft.Extensions.Logging;

namespace Hien_mau.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly Hien_mauContext _context;

        public NotificationController(Hien_mauContext context)
        {
            _context = context;
        }

        // GET: api/NotificationController
        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetNotifications()
        {
            var notifications = await _context.Notifications
                .Include(n => n.User)
                .OrderByDescending(n => n.SentAt)
                .Take(100)
                .Select(n => new
                {
                    n.NotificationId,
                    n.User,
                    UserName = n.User.Name,
                    n.Title,
                    n.Message,
                    n.Type,
                    n.Priority,
                    n.IsRead,
                    n.SentAt
                }).ToListAsync();
            return Ok(notifications);
        }
    }
}
