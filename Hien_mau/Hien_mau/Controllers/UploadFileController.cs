//using Microsoft.AspNetCore.Mvc;

//namespace Hien_mau.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class UploadFileController : Controller
//    {
//        private readonly IWebHostEnvironment _env;// Inject IWebHostEnvironment to access web root path
//        public UploadFileController(IWebHostEnvironment env)
//        {
//            _env = env;
//        }

//        [HttpPost("image")]
//        public async Task<IActionResult> UploadImage([FromForm] IFormFile file)
//        {
//            if (file == null || file.Length == 0)
//                return BadRequest("Không có file được gửi lên.");

//            var uploadsFolder = Path.Combine(_env.WebRootPath, "uploads");
//            if (!Directory.Exists(uploadsFolder))
//                Directory.CreateDirectory(uploadsFolder);

//            var fileName = $"{Guid.NewGuid()}_{Path.GetFileName(file.FileName)}";
//            var filePath = Path.Combine(uploadsFolder, fileName);

//            using (var stream = new FileStream(filePath, FileMode.Create))
//            {
//                await file.CopyToAsync(stream);
//            }

//            var fileUrl = $"{Request.Scheme}://{Request.Host}/uploads/{fileName}";

//            return Ok(new { message = "Tải ảnh thành công!", fileName = fileName, url = fileUrl });
//        }

//        [HttpPost("upload")]
//        public async Task<IActionResult> UploadFile([FromForm] IFormFile file)
//        {
//            if (file == null || file.Length == 0)
//                return BadRequest("Không có file được gửi lên.");

//            // Giới hạn file: chỉ cho ảnh và pdf
//            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".pdf" };
//            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

//            if (!allowedExtensions.Contains(extension))
//                return BadRequest("Chỉ hỗ trợ file ảnh (.jpg, .png) và file PDF.");

//            var uploadsFolder = Path.Combine(_env.WebRootPath, "uploads");
//            if (!Directory.Exists(uploadsFolder))
//                Directory.CreateDirectory(uploadsFolder);

//            var fileName = $"{Guid.NewGuid()}{extension}";
//            var filePath = Path.Combine(uploadsFolder, fileName);

//            using (var stream = new FileStream(filePath, FileMode.Create))
//            {
//                await file.CopyToAsync(stream);
//            }

//            var fileUrl = $"{Request.Scheme}://{Request.Host}/uploads/{fileName}";

//            return Ok(new { message = "Tải file thành công!", fileName = fileName, url = fileUrl });
//        }

//    }
//}
