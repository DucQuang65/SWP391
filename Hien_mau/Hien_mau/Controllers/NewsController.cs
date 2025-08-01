using Hien_mau.Data;
using Hien_mau.Dto;
using Hien_mau.Models;
using Hien_mau.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace Hien_mau.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly Hien_mauContext _context;
        private readonly ActivityLogger _logger;

        public NewsController(Hien_mauContext context, ActivityLogger logger)
        {
            _context = context;
            _logger = logger;
        }

       
        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetBlogPosts()
        {
            var blogs = await _context.Contents
                .Where(c => c.ContentType == "News")
                .Include(p => p.Tags)
                .ToListAsync();

            var response = blogs.Select(p => new
            {
                p.ContentID,
                p.Title,
                p.Content,
                p.ImgUrl,
                p.UserId,
                RoleID = _context.Users
                    .Where(u => u.UserId == p.UserId)
                    .Select(u => u.RoleId)
                    .FirstOrDefault(),
                p.CreatedAt,
                Tags = p.Tags.Select(t => new
                {
                    t.TagId,
                    t.TagName
                }).ToList()
            }).ToList();

            return Ok(response);
        }

       
        [HttpGet("{id}")]
        public async Task<ActionResult<object>> GetBlogPost(int id)
        {
            var blog = await _context.Contents
                 .Where(c => c.ContentType == "News")
                 .Include(p => p.Tags)
                 .Include(p => p.User)
                 .FirstOrDefaultAsync(p => p.ContentID == id);


            if (blog == null)
            {
                return NotFound();
            }

            var response = new
            {
                blog.ContentID,
                blog.Title,
                blog.Content,
                blog.ImgUrl,
                blog.UserId,
                RoleID = await _context.Users
                    .Where(u => u.UserId == blog.UserId)
                    .Select(u => u.RoleId)
                    .FirstOrDefaultAsync(),
                blog.CreatedAt,
                Tags = blog.Tags.Select(t => new
                {
                    t.TagId,
                    t.TagName
                }).ToList()
            };
            return Ok(response);
        }

     
        [HttpPost]
        public async Task<ActionResult<object>> CreateBlogPost([FromBody] ContentsCreateDto dto, [FromServices] ActivityLogger logger)
        {
            // Check the validity of the input data
            if (string.IsNullOrEmpty(dto.Title) || string.IsNullOrEmpty(dto.Content) || dto.UserId <= 0)
            {
                return BadRequest("Title, Content, and UserId are required.");
            }

            // Check if the user exists
            var userExists = await _context.Users.AnyAsync(u => u.UserId == dto.UserId);
            if (!userExists)
            {
                return BadRequest("Invalid UserId.");
            }

            var postedAt = DateTime.UtcNow.AddHours(7); // GMT+7

            var news = new Contents
            {
                Title = dto.Title,
                Content = dto.Content,
                ImgUrl = dto.ImgUrl,
                UserId = dto.UserId,
                ContentType = "News",
                CreatedAt = postedAt
            };

            // Retrieve and assign tags if provided
            if (dto.TagIds != null && dto.TagIds.Any())
            {
                var tags = await _context.Tags
                    .Where(t => dto.TagIds.Contains(t.TagId))
                    .ToListAsync();
                if (tags.Count != dto.TagIds.Count)
                {
                    return BadRequest("One or more TagIds are invalid.");
                }
                news.Tags = tags;
            }

            _context.Contents.Add(news);
            await _context.SaveChangesAsync();
            // Record the activity
            await logger.LogAsync(dto.UserId, "Create", "News", news.ContentID, $"Tạo bài viết: {news.Title}");

            // Resopne with the created blog post
            var response = new
            {
                news.ContentID,
                news.Title,
                news.Content,
                news.ImgUrl,
                news.UserId,
                news.CreatedAt,
                Tags = news.Tags.Select(t => new
                {
                    t.TagId,
                    t.TagName
                }).ToList()
            };

            return CreatedAtAction(nameof(GetBlogPost), new { id = news.ContentID }, response);

        }

      
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBlogPost(int id, [FromBody] ContentsUpdateDto dto, [FromServices] ActivityLogger logger)
        {
            var news = await _context.Contents
                .Where(c => c.ContentType == "News")
                .Include(p => p.Tags)
                .FirstOrDefaultAsync(p => p.ContentID == id);
            if (news == null) return NotFound();

            // check valid input data
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
            // Record the activity
            await logger.LogAsync(dto.UserId, "Update", "News", news.ContentID, $"Cập nhật bài viết: {news.Title}");

            return NoContent();
        }

     
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBlogPost(int id, [FromQuery] int userId, [FromServices] ActivityLogger logger)
        {
            var news = await _context.Contents
                .Where(c => c.ContentType == "News")
                .Include(p => p.Tags)
                .FirstOrDefaultAsync(p => p.ContentID == id);
            if (news == null) return NotFound();


            // Delete tags associated with the news
            news.Tags.Clear();

            _context.Contents.Remove(news);
            await _context.SaveChangesAsync();

            // record the activity
            await logger.LogAsync(userId, "Delete", "News", news.ContentID, $"Xoá bài viết: {news.Title}");

            return NoContent();
        }
    }
}