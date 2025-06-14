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
    public class NewsController : ControllerBase
    {
        private readonly Hien_mauContext _context;

        public NewsController(Hien_mauContext context)
        {
            _context = context;
        }

        // GET: api/BlogPosts (Without Pagination)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetBlogPosts()
        {
            var blogs = await _context.News
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
                    
                    Tags = p.Tags.Select(t => t.TagName).ToList()
                })
                .ToListAsync();
            return Ok(blogs);
        }

        // GET: api/BlogPosts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<object>> GetBlogPost(int id)
        {
            var blog = await _context.News
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
        public async Task<ActionResult<object>> CreateBlogPost([FromBody] NewsCreateDto dto)
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

     
            

            var news = new News
            {
                Title = dto.Title,
                Content = dto.Content,
                ImgUrl = dto.ImgUrl,
                UserId = dto.UserId,
                PostedAt = DateTime.UtcNow,
                
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

            // Trả về đối tượng không chứa vòng lặp
            var response = new
            {
                news.PostId,
                news.Title,
                news.Content,
                news.ImgUrl,
                news.UserId,
                news.PostedAt,
               
                TagNames = news.Tags.Select(t => t.TagName).ToList()
            };

            return CreatedAtAction(nameof(GetBlogPost), new { id = news.PostId }, response);
        }

        // PUT: api/BlogPosts/5
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBlogPost(int id)
        {
            var blog = await _context.News
                .Include(p => p.Tags) // Nạp Tags để xử lý quan hệ
                .FirstOrDefaultAsync(p => p.PostId == id);

            if (blog == null)
            {
                return NotFound();
            }

            // Xóa các bản ghi liên quan trong BlogPostTags
            blog.Tags.Clear();

            _context.News.Remove(blog);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}