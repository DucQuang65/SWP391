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
            var articles = await _context.BloodArticles
        .Include(a => a.Tags)
        .ToListAsync();


            foreach (var article in articles)
            {
                if (article.CreatedAt < new DateTime(2025, 1, 1))
                {
                    article.CreatedAt = DateTime.Now;
                    _context.BloodArticles.Update(article);
                }
            }
            await _context.SaveChangesAsync();

            var response = articles.Select(a => new
            {
                a.ArticleId,
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
            var article = await _context.BloodArticles
         .Include(a => a.Tags)
         .FirstOrDefaultAsync(a => a.ArticleId == id);

            if (article == null)
            {
                return NotFound();
            }

            // Cập nhật CreatedAt nếu là giá trị mặc định ban đầu
            if (article.CreatedAt < new DateTime(2025, 1, 1))
            {
                article.CreatedAt = DateTime.Now;
                _context.BloodArticles.Update(article);
                await _context.SaveChangesAsync();
            }

            var response = new
            {
                article.ArticleId,
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
        public async Task<ActionResult<object>> CreateBloodArticle([FromBody] BloodArticleCreateDto dto, [FromServices] ActivityLogger logger)
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

            var createdAt = DateTime.Now; // Lấy múi giờ địa phương

            var article = new BloodArticles
            {
                Title = dto.Title,
                Content = dto.Content,
                ImgUrl = dto.ImgUrl,
                UserId = dto.UserId,
                CreatedAt = createdAt
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

            _context.BloodArticles.Add(article);
            await _context.SaveChangesAsync();
            // Ghi log
            await logger.LogAsync(dto.UserId, "Create", "Article", article.ArticleId, $"Tạo bài viết: {dto.Title}");

            // Trả về đối tượng không chứa vòng lặp
            var response = new
            {
                article.ArticleId,
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

            return CreatedAtAction(nameof(GetBloodArticle), new { id = article.ArticleId }, response);
        }

        // PUT: api/BloodArticles/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBloodArticle(int id, [FromBody] BloodArticleUpdateDto dto, [FromServices] ActivityLogger logger)
        {
            var article = await _context.BloodArticles
                .Include(a => a.Tags)
                .FirstOrDefaultAsync(a => a.ArticleId == id);

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
            await logger.LogAsync(dto.UserId, "Update", "Article", article.ArticleId, $"Cập nhật bài viết: {dto.Title}");
            return NoContent();
        }

        // DELETE: api/BloodArticles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBloodArticle(int id, [FromQuery] int userId, [FromServices] ActivityLogger logger)
        {
            var article = await _context.BloodArticles
                .Include(a => a.Tags)
                .FirstOrDefaultAsync(a => a.ArticleId == id);

            if (article == null)
            {
                return NotFound();
            }

            // Xóa các bản ghi liên quan trong ArticleTags
            article.Tags.Clear();

            _context.BloodArticles.Remove(article);
            await _context.SaveChangesAsync();
            // Ghi log
            await logger.LogAsync(userId, "Delete", "Article", article.ArticleId, $"Xoá bài viết: {article.Title}");
            return NoContent();
        }
    }
}