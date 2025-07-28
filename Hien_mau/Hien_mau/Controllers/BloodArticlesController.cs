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
    public class BloodArticlesController : ControllerBase
    {
        private readonly Hien_mauContext _context;

        public BloodArticlesController(Hien_mauContext context)
        {
            _context = context;
        }

        // GET: api/BloodArticles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetBloodArticles()
        {
            var articles = await _context.Contents
                .Where(c => c.ContentType == "Article")
                .Include(a => a.Tags)
                .ToListAsync();

            await _context.SaveChangesAsync();

            var response = articles.Select(a => new
            {
                a.ContentID,
                a.Title,
                a.Content,
                a.ImgUrl,
                a.CreatedAt,
                a.UserId,
                RoleID = _context.Users
                    .Where(u => u.UserId == a.UserId)
                    .Select(u => u.RoleId)
                    .FirstOrDefault(),
                Tags = a.Tags.Select(t => new
                {
                    t.TagId,
                    t.TagName
                }).ToList()
            }).ToList();

            return Ok(response);
        }

        // GET: api/BloodArticles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<object>> GetBloodArticle(int id)
        {
            var article = await _context.Contents
                .Where(c => c.ContentType == "Article")
                .Include(a => a.Tags)
                .FirstOrDefaultAsync(a => a.ContentID == id);

            if (article == null)
            {
                return NotFound();
            }

            var response = new
            {
                article.ContentID,
                article.Title,
                article.Content,
                article.ImgUrl,
                article.CreatedAt,
                article.UserId,
                RoleID = await _context.Users
                    .Where(u => u.UserId == article.UserId)
                    .Select(u => u.RoleId)
                    .FirstOrDefaultAsync(),
                Tags = article.Tags.Select(t => new
                {
                    t.TagId,
                    t.TagName
                }).ToList()
            };

            return Ok(response);
        }

        // POST: api/BloodArticles
        [HttpPost]
        public async Task<ActionResult<object>> CreateBloodArticle([FromBody] ContentsCreateDto dto, [FromServices] ActivityLogger logger)
        {
            // Kiểm tra dữ liệu đầu vào
            if (string.IsNullOrEmpty(dto.Title) || string.IsNullOrEmpty(dto.Content) || dto.UserId <= 0)
            {
                return BadRequest("Title, Content, and UserId are required.");
            }

            var userExists = await _context.Users.AnyAsync(u => u.UserId == dto.UserId);
            if (!userExists)
            {
                return BadRequest($"UserId {dto.UserId} does not exist in Users table.");
            }

            var postedAt = DateTime.UtcNow.AddHours(7); // Giờ GMT+7

            var article = new Contents
            {
                Title = dto.Title,
                Content = dto.Content,
                ImgUrl = dto.ImgUrl,
                UserId = dto.UserId,
                ContentType = "Article",
                CreatedAt = postedAt
            };

            // Chỉ truy vấn Tags nếu cần
            if (dto.TagIds != null && dto.TagIds.Any())
            {
                var tags = await _context.Tags
                    .Where(t => dto.TagIds.Contains(t.TagId))
                    .ToListAsync();
                if (tags.Count != dto.TagIds.Count)
                {
                    return BadRequest("One or more TagIds are invalid.");
                }
                article.Tags = tags;
            }

            _context.Contents.Add(article);
            await _context.SaveChangesAsync();
            // Ghi log
            await logger.LogAsync(dto.UserId, "Create", "Article", article.ContentID, $"Tạo bài viết: {dto.Title}");

            // Trả về đối tượng không chứa vòng lặp
            var response = new
            {
                article.ContentID,
                article.Title,
                article.Content,
                article.ImgUrl,
                article.CreatedAt,
                article.UserId,
                RoleID = await _context.Users
                    .Where(u => u.UserId == article.UserId)
                    .Select(u => u.RoleId)
                    .FirstOrDefaultAsync(),
                Tags = article.Tags.Select(t => new
                {
                    t.TagId,
                    t.TagName
                }).ToList()
            };

            return CreatedAtAction(nameof(GetBloodArticle), new { id = article.ContentID }, response);
        }

        // PUT: api/BloodArticles/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBloodArticle(int id, [FromBody] ContentsUpdateDto dto, [FromServices] ActivityLogger logger)
        {
            var article = await _context.Contents
                .Where(c => c.ContentType == "Article")
                .Include(a => a.Tags)
                .FirstOrDefaultAsync(a => a.ContentID == id);

            if (article == null)
            {
                return NotFound();
            }

            // Kiểm tra dữ liệu đầu vào
            if (string.IsNullOrEmpty(dto.Title) || string.IsNullOrEmpty(dto.Content))
            {
                return BadRequest("Title and Content are required.");
            }

            article.Title = dto.Title;
            article.Content = dto.Content;
            article.ImgUrl = dto.ImgUrl;

            if (dto.TagIds != null)
            {
                article.Tags.Clear();
                var tags = await _context.Tags
                    .Where(t => dto.TagIds.Contains(t.TagId))
                    .ToListAsync();
                if (tags.Count != dto.TagIds.Count)
                {
                    return BadRequest("One or more TagIds are invalid.");
                }
                article.Tags = tags;
            }

            await _context.SaveChangesAsync();
            // Ghi log
            await logger.LogAsync(dto.UserId, "Update", "Article", article.ContentID, $"Cập nhật bài viết: {dto.Title}");
            return NoContent();
        }

        // DELETE: api/BloodArticles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBloodArticle(int id, [FromQuery] int userId, [FromServices] ActivityLogger logger)
        {
            var article = await _context.Contents
                .Where(c => c.ContentType == "Article")
                .Include(a => a.Tags)
                .FirstOrDefaultAsync(a => a.ContentID == id);

            if (article == null)
            {
                return NotFound();
            }

            // Xóa các bản ghi liên quan trong ArticleTags
            article.Tags.Clear();

            _context.Contents.Remove(article);
            await _context.SaveChangesAsync();
            // Ghi log
            await logger.LogAsync(userId, "Delete", "Article", article.ContentID, $"Xoá bài viết: {article.Title}");
            return NoContent();
        }
    }
}