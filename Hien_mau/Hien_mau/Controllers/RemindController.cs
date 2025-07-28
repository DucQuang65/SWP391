using Hien_mau.Data;
using Hien_mau.DTOs;
using Hien_mau.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hien_mau.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RemindController : ControllerBase
    {
        private readonly Hien_mauContext _context;

        public RemindController(Hien_mauContext context)
        {
            _context = context;
        }

      
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<ReminderDto>>> GetRemindersByUser(int userId)
        {
            var reminders = await _context.Reminders
                .Include(r => r.User)
                .Where(r => r.UserId == userId)
                .OrderByDescending(r => r.RemindAt)
                .Select(r => new ReminderDto
                {
                    ReminderId = r.ReminderId,
                    UserId = r.UserId,
                    Type = r.Type,
                    Message = r.Message,
                    RemindAt = r.RemindAt,
                    IsDisabled = r.IsDisabled,
                    CreatedAt = r.CreatedAt,
                    IsSent = r.IsSent,
                    SentAt = r.SentAt,
                    UserFullName = r.User.Name
                })
                .ToListAsync();

            return Ok(reminders);
        }

       
        [HttpPost]
        public async Task<ActionResult> CreateReminder([FromBody] ReminderCreateDto dto)
        {
            var userExists = await _context.Users.AnyAsync(u => u.UserId == dto.UserId);
            if (!userExists)
            {
                return NotFound(new { message = "User not found" });
            }

            var reminder = new Reminder
            {
                UserId = dto.UserId,
                Type = dto.Type,
                Message = dto.Message,
                RemindAt = dto.RemindAt,
                CreatedAt = DateTime.Now,
                IsDisabled = false,
                IsSent = false
            };

            _context.Reminders.Add(reminder);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRemindersByUser), new { userId = dto.UserId }, reminder);
        }

        
        [HttpPatch("disable/{id}")]
        public async Task<IActionResult> DisableReminder(int id)
        {
            var reminder = await _context.Reminders.FindAsync(id);
            if (reminder == null) return NotFound();

            reminder.IsDisabled = true;
            await _context.SaveChangesAsync();

            return NoContent();
        }

      
        [HttpPatch("enable/{id}")]
        public async Task<IActionResult> EnableReminder(int id)
        {
            var reminder = await _context.Reminders.FindAsync(id);
            if (reminder == null) return NotFound();

            reminder.IsDisabled = false;
            await _context.SaveChangesAsync();

            return NoContent();
        }

      
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReminder(int id)
        {
            var reminder = await _context.Reminders.FindAsync(id);
            if (reminder == null) return NotFound();

            _context.Reminders.Remove(reminder);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
