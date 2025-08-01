using Hien_mau.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Hien_mau.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UploadFileController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;

        public UploadFileController(IWebHostEnvironment env)
        {
            _env = env;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload([FromForm] FileUploadDto dto)
        {
            if (dto.File == null || dto.File.Length == 0)
                return BadRequest("Không có file được gửi lên.");

            // Allow only specific file types
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".pdf" };
            var extension = Path.GetExtension(dto.File.FileName).ToLowerInvariant();

            if (!allowedExtensions.Contains(extension))
                return BadRequest("Chỉ hỗ trợ file ảnh (.jpg, .png, .gif) và PDF.");

            // Ensure the uploads directory exists
            var uploadFolder = Path.Combine(_env.WebRootPath, "uploads");
            if (!Directory.Exists(uploadFolder))
                Directory.CreateDirectory(uploadFolder);

            // Named file with a unique identifier
            var fileName = $"{Guid.NewGuid()}{extension}";// Guid.NewGuid() generate random name
            var filePath = Path.Combine(uploadFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await dto.File.CopyToAsync(stream);
            }

            // Return the file URL
            var fileUrl = $"{Request.Scheme}://{Request.Host}/uploads/{fileName}";

            return Ok(new
            {
                message = "Tải file thành công!",
                fileName = fileName,
                url = fileUrl
            });
        }
    }
}
