using Hien_mau.Data;
using Hien_mau.Dto;
using Hien_mau.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hien_mau.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostsController : ControllerBase
    {
        private readonly Hien_mauContext _context;

        public BlogPostsController(Hien_mauContext context)
        {
            _context = context;
        }

        // GET: api/BlogPosts (Without Pagination)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetBlogPosts()
        {
            var blogs = await _context.BlogPosts
                .Select(p => new
                {
                    p.PostId,
                    p.Title,
                    p.Content,
                    p.ImgUrl,
                    UserName = _context.Users
                        .Where(u => u.UserId == p.UserId)
                        .Select(u => u.Name)
                        .FirstOrDefault(),
                    p.PostedAt,
                    p.Status, // Status là byte
                    Tags = p.Tags.Select(t => t.TagName).ToList()
                })
                .ToListAsync();
            return Ok(blogs);
        }

        // GET: api/BlogPosts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<object>> GetBlogPost(int id)
        {
            var blog = await _context.BlogPosts
                .Where(p => p.PostId == id)
                .Select(p => new
                {
                    p.PostId,
                    p.Title,
                    p.Content,
                    p.ImgUrl,
                    UserName = _context.Users
                        .Where(u => u.UserId == p.UserId)
                        .Select(u => u.Name)
                        .FirstOrDefault(),
                    p.PostedAt,
                    p.Status, // Status là byte
                    Tags = p.Tags.Select(t => t.TagName).ToList()
                })
                .FirstOrDefaultAsync();

            if (blog == null)
            {
                return NotFound();
            }
            return Ok(blog);
        }

        // POST: api/BlogPosts
        [HttpPost]
        public async Task<ActionResult<object>> CreateBlogPost([FromBody] BlogPostCreateDto dto)
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

            // Kiểm tra Status hợp lệ
            if (dto.Status.HasValue && dto.Status != 0 && dto.Status != 1 && dto.Status != 2)
            {
                return BadRequest("Status must be 0, 1, or 2.");
            }

            var blog = new BlogPost
            {
                Title = dto.Title,
                Content = dto.Content,
                ImgUrl = dto.ImgUrl,
                UserId = dto.UserId,
                PostedAt = DateTime.UtcNow,
                Status = dto.Status ?? 0 // Status là byte
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
                blog.Tags = tags;
            }

            _context.BlogPosts.Add(blog);
            await _context.SaveChangesAsync();

            // Trả về đối tượng không chứa vòng lặp
            var response = new
            {
                blog.PostId,
                blog.Title,
                blog.Content,
                blog.ImgUrl,
                blog.UserId,
                blog.PostedAt,
                blog.Status, // Status là byte
                TagNames = blog.Tags.Select(t => t.TagName).ToList()
            };

            return CreatedAtAction(nameof(GetBlogPost), new { id = blog.PostId }, response);
        }

        // PUT: api/BlogPosts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBlogPost(int id, [FromBody] BlogPostUpdateDto dto)
        {
            var blog = await _context.BlogPosts
                .Include(p => p.Tags)
                .FirstOrDefaultAsync(p => p.PostId == id);

            if (blog == null)
            {
                return NotFound();
            }

            // Kiểm tra Status hợp lệ
            if (dto.Status.HasValue && dto.Status != 0 && dto.Status != 1 && dto.Status != 2)
            {
                return BadRequest("Status must be 0, 1, or 2.");
            }

            blog.Title = dto.Title;
            blog.Content = dto.Content;
            blog.ImgUrl = dto.ImgUrl;
            blog.Status = dto.Status ?? blog.Status;

            if (dto.TagIds != null)
            {
                blog.Tags.Clear();
                var tags = await _context.Tags
                    .Where(t => dto.TagIds.Contains(t.TagId))
                    .ToListAsync();
                if (tags.Count != dto.TagIds.Count)
                {
                    return BadRequest("One or more TagIds are invalid.");
                }
                blog.Tags = tags;
            }

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBlogPost(int id)
        {
            var blog = await _context.BlogPosts
                .Include(p => p.Tags) // Nạp Tags để xử lý quan hệ
                .FirstOrDefaultAsync(p => p.PostId == id);

            if (blog == null)
            {
                return NotFound();
            }

            // Xóa các bản ghi liên quan trong BlogPostTags
            blog.Tags.Clear();

            _context.BlogPosts.Remove(blog);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}