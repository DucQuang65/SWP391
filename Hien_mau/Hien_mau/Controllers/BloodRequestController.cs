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
    public class BloodRequestController : ControllerBase
    {
        private readonly Hien_mauContext _context;
        private readonly NotificationLog _logger;
        private readonly IWebHostEnvironment _env;
        public BloodRequestController(Hien_mauContext context, NotificationLog logger, IWebHostEnvironment env)
        {
            _context = context;
            _logger = logger;
            _env = env;

        }
        [HttpGet]
        public async Task<ActionResult<List<BloodRequestDto>>> GetBloodRequest()
        {
            var bloodRequests = await _context.BloodRequests
                .Select(x => new BloodRequestDto
                {
                    UserId = x.UserId,
                    RequestId = x.RequestId,
                    PatientId = x.PatientId,
                    PatientName = x.PatientName,
                    Age = x.Age,
                    Gender = x.Gender,
                    Relationship = x.Relationship,
                    FacilityName = x.FacilityName,
                    DoctorName = x.DoctorName,
                    DoctorPhone = x.DoctorPhone,
                    BloodGroup = x.BloodGroup,
                    RhType = x.RhType,
                    Quantity = x.Quantity,
                    ComponentId = x.ComponentId,
                    Reason = x.Reason,
                    Status = x.Status,
                    CreatedTime = x.CreatedTime
                }).ToListAsync();
            return Ok(bloodRequests);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<BloodRequests>> GetRequestById(int id)
        {
            var bloodRequests = await _context.BloodRequests
                .Select(x => new BloodRequestDto
                {
                    UserId = x.UserId,
                    RequestId = x.RequestId,
                    PatientId = x.PatientId,
                    PatientName = x.PatientName,
                    Age = x.Age,
                    Gender = x.Gender,
                    Relationship = x.Relationship,
                    FacilityName = x.FacilityName,
                    DoctorName = x.DoctorName,
                    DoctorPhone = x.DoctorPhone,
                    BloodGroup = x.BloodGroup,
                    RhType = x.RhType,
                    ComponentId = x.ComponentId,
                    Quantity = x.Quantity,
                    Reason = x.Reason,
                    Status = x.Status,
                    CreatedTime = x.CreatedTime
                }).FirstOrDefaultAsync();

            if (bloodRequests == null) 
                return NotFound();

            return Ok(bloodRequests);
        }
        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> CreateBloodRequest([FromForm] BloodRequestDto bloodRequestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }           

            var existingRequest = await _context.BloodRequests
            .Where(x => x.UserId == bloodRequestDto.UserId && x.Status == 0) 
            .FirstOrDefaultAsync();

            if (existingRequest != null)
            {
                return BadRequest();
            }

            string? uploadedFileName = null;

            if (bloodRequestDto.MedicalFile != null)
            {
                var allowedExtensions = new[] { ".pdf" };
                var extension = Path.GetExtension(bloodRequestDto.MedicalFile.FileName).ToLowerInvariant();
                if (!allowedExtensions.Contains(extension))
                    return BadRequest("Chỉ chấp nhận file PDF");

                var uploadsFolder = Path.Combine(_env.WebRootPath, "uploads");
                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                uploadedFileName = $"{Guid.NewGuid()}{extension}";
                var filePath = Path.Combine(uploadsFolder, uploadedFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await bloodRequestDto.MedicalFile.CopyToAsync(stream);
                }
            }

            var vietNamTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
            var vietNamTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, vietNamTimeZone);

            var bloodRequest = new BloodRequests
            {
                UserId = bloodRequestDto.UserId,
                PatientId = bloodRequestDto.PatientId,
                PatientName = bloodRequestDto.PatientName,
                Age = bloodRequestDto.Age,
                Gender = bloodRequestDto.Gender,
                Relationship = bloodRequestDto.Relationship,
                BloodGroup = bloodRequestDto.BloodGroup,
                RhType = bloodRequestDto.RhType,
                ComponentId = bloodRequestDto.ComponentId,
                Quantity = bloodRequestDto.Quantity,
                Reason = bloodRequestDto.Reason,
                CreatedTime = vietNamTime,
                MedicalReport = uploadedFileName != null ? $"/uploads/{uploadedFileName}" : null
            };

            if (bloodRequestDto.Relationship == "Bác sĩ phụ trách")
            {
                var doctorUser = await _context.Users.FindAsync(bloodRequestDto.UserId);
                if (doctorUser != null)
                {
                    bloodRequest.DoctorName = doctorUser.Name;
                    bloodRequest.DoctorPhone = doctorUser.Phone;
                    bloodRequest.Status = 1;
                    bloodRequest.FacilityName = "Ánh Dương";
                }
            }
            else
            {
                bloodRequest.Status = 0; 
                bloodRequest.DoctorName = bloodRequestDto.DoctorName;
                bloodRequest.DoctorPhone = bloodRequestDto.DoctorPhone;
                bloodRequest.FacilityName = bloodRequestDto.FacilityName;
            }

            _context.BloodRequests.Add(bloodRequest);
            await _context.SaveChangesAsync();
            // Ghi log sau khi đã lưu với UserId tương ứng
            await _logger.NotiLog(bloodRequest.UserId, "Yêu cầu máu", $"Tạo yêu cầu:", "Create");

            bloodRequestDto.RequestId = bloodRequest.RequestId;
            bloodRequestDto.Status = bloodRequest.Status;
            bloodRequestDto.CreatedTime = bloodRequest.CreatedTime; 
            bloodRequestDto.MedicalReportUrl = bloodRequest.MedicalReport;

            return CreatedAtAction(nameof(GetRequestById), new { id = bloodRequest.RequestId }, bloodRequestDto);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBloodRequest(int id, BloodRequestDto bloodRequestDto)
        {
            var bloodRequest = await _context.BloodRequests.FindAsync(id);
            if (bloodRequest == null)
                return NotFound();

            if (bloodRequest.Status == 2 || bloodRequest.Status == 3 || bloodRequest.Status == 4)
                return BadRequest();

            bloodRequest.PatientId = bloodRequestDto.PatientId;
            bloodRequest.PatientName = bloodRequestDto.PatientName;
            bloodRequest.Age = bloodRequestDto.Age;
            bloodRequest.Gender = bloodRequestDto.Gender;
            bloodRequest.Relationship = bloodRequestDto.Relationship;
            bloodRequest.FacilityName = bloodRequestDto.FacilityName;
            bloodRequest.DoctorName = bloodRequestDto.DoctorName;
            bloodRequest.DoctorPhone = bloodRequestDto.DoctorPhone;
            bloodRequest.BloodGroup = bloodRequestDto.BloodGroup;
            bloodRequest.RhType = bloodRequestDto.RhType;
            bloodRequest.ComponentId = bloodRequestDto.ComponentId;
            bloodRequest.Quantity = bloodRequestDto.Quantity;
            bloodRequest.Reason = bloodRequestDto.Reason;


            _context.BloodRequests.Update(bloodRequest);
            await _context.SaveChangesAsync();
            // Ghi log sau khi đã lưu với UserId tương ứng
            await _logger.NotiLog(bloodRequest.UserId, "Yêu cầu máu", $"Sửa yêu cầu:", "Update");
            return Ok();
        }

        [HttpPatch("{id}/status")]
        public async Task<IActionResult> PatchDistance(int id, UpdateStatusBloodRequestDto updateStatusBloodRequestDto)
        {
            var bloodRequest = await _context.BloodRequests.FindAsync(id);
            if (bloodRequest == null)
            {
                return NotFound();
            }

            bloodRequest.Status = updateStatusBloodRequestDto.Status;
            _context.BloodRequests.Update(bloodRequest);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBloodRequest(int id)
        {
            var bloodRequest = await _context.BloodRequests.FindAsync(id);
            if (bloodRequest == null)
                return NotFound();

            bloodRequest.Status = 4;

            _context.BloodRequests.Update(bloodRequest);
            await _context.SaveChangesAsync();
            // Ghi log sau khi đã lưu với UserId tương ứng
            await _logger.NotiLog(bloodRequest.UserId, "Yêu cầu máu", $"Xóa yêu cầu:", "Delete");

            return Ok();
        }
    }
}
