using Hien_mau.Data;
using Hien_mau.Dto;
using Hien_mau.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

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
        // GET: api/News
        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetActivityLog()   
        {
            var logs = await _context.ActivityLogs
                .Include(l => l.User)//Eager loading: retrives the User details associated with each log
                .OrderByDescending(l => l.CreatedAt)// Display the latest log first
                .ToListAsync();

            //Ensuring the CreatedAt field is updated if it is before a certain date
            foreach (var log in logs)
            {
                if (log.CreatedAt <  new DateTime(2025, 6, 1))
                {
                    log.CreatedAt = DateTime.Now;
                    _context.ActivityLogs.Update(log);
                }
            }
            await _context.SaveChangesAsync();

            var response = logs.Select(p => new
            {
                p.LogId,
                p.UserID,
                p.ActivityType,
                p.EntityId,
                p.EntityType,
                p.Description,
                p.CreatedAt,
            }).ToList();

            return Ok(response);
        }

        // GET: api/News/5
        [HttpGet("{id}")]
        public async Task<ActionResult<object>> GetBlogPost(int id)
        {
            var log = await _context.News
                .FirstOrDefaultAsync(p => p.PostId == id);

            if (log == null)
            {
                return NotFound();
            }

            //Ensuring the CreatedAt field is updated if it is before a certain date
            if (log.PostedAt < new DateTime(2025, 1, 1))
            {
                log.PostedAt = DateTime.Now;
                _context.News.Update(log);
                await _context.SaveChangesAsync();
            }

            var response = new
            {
                log.PostId,
                log.Title,
                log.Content,
                log.ImgUrl,
                log.UserId,
                RoleID = await _context.Users
                    .Where(u => u.UserId == log.UserId)
                    .Select(u => u.RoleId)
                    .FirstOrDefaultAsync(),
                log.PostedAt,
                TagNames = log.Tags.Select(t => t.TagName).ToList()
            };

            return Ok(response);
        }

        // POST: api/News
        [HttpPost]
        public async Task<ActionResult<object>> CreateActivityLog([FromBody] LogCreateDto dto)
        {
            // Kiểm tra dữ liệu đầu vào
            if (string.IsNullOrEmpty(dto.ActivityType) || string.IsNullOrEmpty(dto.EntityType) || dto.UserID <= 0)
            {
                return BadRequest("ActivityType, EntityType, and UserID are required.");
            }

            var log = new ActivityLog
            {
                UserID = dto.UserID,
                ActivityType = dto.ActivityType,
                EntityId = dto.EntityId,
                EntityType = dto.EntityType,
                Description = dto.Description,
                CreatedAt = DateTime.Now
            };

            _context.ActivityLogs.Add(log);
            await _context.SaveChangesAsync();

            // Trả về đối tượng không chứa vòng lặp
            var response = new
            {
                log.LogId,
                log.UserID,
                log.ActivityType,
                log.EntityId,
                log.EntityType,
                log.Description,
                log.CreatedAt,
            };

            return CreatedAtAction(nameof(GetActivityLog), new { id = log.LogId }, response);
        }

        // PUT: api/News/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBlogPost(int id, [FromBody] NewsUpdateDto dto)
        {
            var news = await _context.News
                .Include(p => p.Tags)
                .FirstOrDefaultAsync(p => p.PostId == id);

            if (news == null)
            {
                return NotFound();
            }

            // Kiểm tra dữ liệu đầu vào
            if (string.IsNullOrEmpty(dto.Title) || string.IsNullOrEmpty(dto.Content))
            {
                return BadRequest("Title and Content are required.");
            }

            news.Title = dto.Title;
            news.Content = dto.Content;
            news.ImgUrl = dto.ImgUrl;

            if (dto.TagIds != null)
            {
                news.Tags.Clear();
                var tags = await _context.Tags
                    .Where(t => dto.TagIds.Contains(t.TagId))
                    .ToListAsync();
                if (tags.Count != dto.TagIds.Count)
                {
                    return BadRequest("One or more TagIds are invalid.");
                }
                news.Tags = tags;
            }

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/News/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBlogPost(int id)
        {
            var log = await _context.News
                .Include(p => p.Tags)
                .FirstOrDefaultAsync(p => p.PostId == id);

            if (log == null)
            {
                return NotFound();
            }

            // Xóa các bản ghi liên quan trong NewsTags
            log.Tags.Clear();

            _context.News.Remove(log);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
