using Hien_mau.Data;
using Hien_mau.Dto;
using Hien_mau.Models;
using Hien_mau.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;

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

        // GET: api/News
        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetBlogPosts()
        {
            var blogs = await _context.News
                .Include(p => p.Tags)
                .Include(p => p.User)
                .ToListAsync();

            //// Cập nhật PostedAt nếu là giá trị mặc định ban đầu
            //foreach (var blog in blogs)
            //{
            //    if (blog.PostedAt < new DateTime(2025, 6, 1))
            //    {
            //        blog.PostedAt = DateTime.Now;
            //        _context.News.Update(blog);
            //    }
            //}
            //await _context.SaveChangesAsync();

            var response = blogs.Select(p => new
            {
                p.PostId,
                p.Title,
                p.Content,
                p.ImgUrl,
                p.UserId,
                RoleID = _context.Users
                    .Where(u => u.UserId == p.UserId)
                    .Select(u => u.RoleId)
                    .FirstOrDefault(),
                p.PostedAt,
                Tags = p.Tags.Select(t => new
                {
                    t.TagId,
                    t.TagName
                }).ToList()
            }).ToList();

            return Ok(response);
        }

        // GET: api/News/5
        [HttpGet("{id}")]
        public async Task<ActionResult<object>> GetBlogPost(int id)
        {
            var blog = await _context.News
                 .Include(p => p.Tags)
                 .Include(p => p.User)
                 .FirstOrDefaultAsync(p => p.PostId == id);


            if (blog == null)
            {
                return NotFound();
            }

            //// Cập nhật PostedAt nếu là giá trị mặc định ban đầu
            //if (blog.PostedAt < new DateTime(2025, 1, 1))
            //{
            //    blog.PostedAt = DateTime.Now;
            //    _context.News.Update(blog);
            //    await _context.SaveChangesAsync();
            //}

            var response = new
            {
                blog.PostId,
                blog.Title,
                blog.Content,
                blog.ImgUrl,
                blog.UserId,
                RoleID = await _context.Users
                    .Where(u => u.UserId == blog.UserId)
                    .Select(u => u.RoleId)
                    .FirstOrDefaultAsync(),
                blog.PostedAt,
                Tags = blog.Tags.Select(t => new
                {
                    t.TagId,
                    t.TagName
                }).ToList()
            };
            return Ok(response);
        }

        // POST: api/News
        [HttpPost]
        public async Task<ActionResult<object>> CreateBlogPost([FromBody] NewsCreateDto dto, [FromServices] ActivityLogger logger)
        {
            // Kiểm tra dữ liệu đầu vào
            if (string.IsNullOrEmpty(dto.Title) || string.IsNullOrEmpty(dto.Content) || dto.UserId <= 0)
            {
                return BadRequest("Title, Content, and UserId are required.");
            }

            // Kiểm tra UserId tồn tại
            var userExists = await _context.Users.AnyAsync(u => u.UserId == dto.UserId);
            if (!userExists)
            {
                return BadRequest("Invalid UserId.");
            }

            var postedAt = DateTime.Now; // Lấy múi giờ địa phương

            var news = new News
            {
                Title = dto.Title,
                Content = dto.Content,
                ImgUrl = dto.ImgUrl,
                UserId = dto.UserId,
                PostedAt = postedAt
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
                news.Tags = tags;
            }

            _context.News.Add(news);
            await _context.SaveChangesAsync();
            // Ghi log SAU khi đã lưu
            await logger.LogAsync(dto.UserId, "Create", "News", news.PostId, $"Tạo bài viết: {news.Title}");

            // Trả về đối tượng không chứa vòng lặp
            var response = new
            {
                news.PostId,
                news.Title,
                news.Content,
                news.ImgUrl,
                news.UserId,
                news.PostedAt,
                Tags = news.Tags.Select(t => new
                {
                    t.TagId,
                    t.TagName
                }).ToList()
            };

            return CreatedAtAction(nameof(GetBlogPost), new { id = news.PostId }, response);

        }

        // PUT: api/News/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBlogPost(int id, [FromBody] NewsUpdateDto dto, [FromServices] ActivityLogger logger)
        {
            var news = await _context.News.Include(p => p.Tags).FirstOrDefaultAsync(p => p.PostId == id);
            if (news == null) return NotFound();

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
            // Ghi log
            await logger.LogAsync(dto.UserId, "Update", "News", news.PostId, $"Cập nhật bài viết: {news.Title}");

            return NoContent();
        }

        // DELETE: api/News/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBlogPost(int id, [FromQuery] int userId, [FromServices] ActivityLogger logger)
{
            var news = await _context.News.Include(p => p.Tags).FirstOrDefaultAsync(p => p.PostId == id);
            if (news == null) return NotFound();


            // Xóa các bản ghi liên quan trong NewsTags
            news.Tags.Clear();

            _context.News.Remove(news);
            await _context.SaveChangesAsync();

            // Ghi log
            await logger.LogAsync(userId, "Delete", "News", news.PostId, $"Xoá bài viết: {news.Title}");

            return NoContent();
        }
    }
}