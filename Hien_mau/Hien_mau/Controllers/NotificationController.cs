using Azure.Core;
using Hien_mau.Data;
using Hien_mau.Dto;
using Hien_mau.Models;
using Hien_mau.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        // GET: api/Notification
        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetNotifications()
        {
            var notifications = await _context.Notifications
                .Include(n => n.User)
                .OrderByDescending(n => n.SentAt)
                .Select(n => new
                {
                    n.NotificationId,
                    n.UserId,
                    UserName = n.User.Name,
                    n.Title,
                    n.Message,
                    n.Type,
                    n.Priority,
                    n.IsRead,
                    n.SentAt
                })
                .ToListAsync();

            return Ok(notifications);
        }

        // GET: api/Notification/5
        [HttpGet("{id}")]
        public async Task<ActionResult<object>> GetNotification(int id)
        {
            var notification = await _context.Notifications
                .Include(n => n.User)
                .FirstOrDefaultAsync(n => n.NotificationId == id);

            if (notification == null)
                return NotFound();

            return Ok(new
            {
                notification.NotificationId,
                notification.UserId,
                UserName = notification.User.Name,
                notification.Title,
                notification.Message,
                notification.Type,
                notification.Priority,
                notification.IsRead,
                notification.SentAt
            });
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<object>>> GetNotificationsByUser(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
                return NotFound("User not found.");

            var notifications = await _context.Notifications
                .Where(n => n.UserId == userId)
                .OrderByDescending(n => n.SentAt)
                .Select(n => new
                {
                    n.NotificationId,
                    n.Title,
                    n.Message,
                    n.Type,
                    n.Priority,
                    n.IsRead,
                    n.SentAt
                }).ToListAsync();

            return Ok(notifications);
        }

        // PUT: api/Notification/mark-read/5
        [HttpPut("mark-read/{id}")]
        public async Task<IActionResult> MarkAsRead(int id)
        {
            var notification = await _context.Notifications.FindAsync(id);
            if (notification == null)
                return NotFound();

            notification.IsRead = true;
            await _context.SaveChangesAsync();

            return NoContent();
        }


        // POST: api/Notification
        [HttpPost]
        public async Task<ActionResult<object>> CreateNotification([FromBody] NotificationDto dto)
        {
            var user = await _context.Users.FindAsync(dto.UserId);
            if (user == null)
            {
                return NotFound(new { error = "User does not exist" });
            }

            var notification = new Notifications
            {
                UserId = dto.UserId,
                Title = dto.Title!,
                Message = dto.Message!,
                Type = dto.Type!,
                Priority = dto.Priority,
                IsRead = false,
                SentAt = DateTime.Now
            };

            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetNotification), new { id = notification.NotificationId }, notification);
        }

        // PUT: api/Notification/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateNotification(int id, [FromBody] NotificationUpdateDto dto)
        {
            var notification = await _context.Notifications.FindAsync(id);
            if (notification == null) return NotFound(new { error = "Notification not found." });

            // Chỉ cập nhật những trường được gửi lên
            if (!string.IsNullOrWhiteSpace(dto.Title))
            {
                notification.Title = dto.Title;
            }

            if (!string.IsNullOrWhiteSpace(dto.Message))
            {
                notification.Message = dto.Message;
            }

            if (!string.IsNullOrWhiteSpace(dto.Type))
            {
                var validTypes = new[] { "Reminder", "Alert", "Report" };
                if (!validTypes.Contains(dto.Type))
                {
                    return BadRequest(new { error = "Invalid notification type. Must be 'Reminder', 'Alert', or 'Report'." });
                }
                notification.Type = dto.Type;
            }

            // Priority phải là 0 hoặc 1 nếu muốn cập nhật
            //if (dto.Priority >= 0 && dto.Priority <= 1)
            //{
            //    notification.Priority = dto.Priority;
            //}

            // Cập nhật trạng thái đã đọc
            notification.IsRead = dto.IsRead;

            // Cập nhật thời gian sửa đổi
            notification.SentAt = DateTime.Now;

            await _context.SaveChangesAsync();

            return Ok(new
            {
                notification.NotificationId,
                notification.UserId,
                notification.Title,
                notification.Message,
                notification.Type,
                notification.Priority,
                notification.IsRead,
                notification.SentAt
            });
        }


        // DELETE: api/Notification/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNotification(int id)
        {
            var notification = await _context.Notifications.FindAsync(id);
            if (notification == null) return NotFound();

            _context.Notifications.Remove(notification);
            await _context.SaveChangesAsync(); 

            return NoContent();
        }
    }
}
