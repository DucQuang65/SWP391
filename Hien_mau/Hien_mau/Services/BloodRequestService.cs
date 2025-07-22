using Hien_mau.Data;
using Hien_mau.Dto;
using Hien_mau.Interface;
using Hien_mau.Models;
using Microsoft.EntityFrameworkCore;

namespace Hien_mau.Services
{
    public class BloodRequestService : IBloodRequestService
    {
        private readonly Hien_mauContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly NotificationLog _logger;

        public BloodRequestService(Hien_mauContext context, IWebHostEnvironment env, NotificationLog logger)
        {
            _context = context;
            _env = env;
            _logger = logger;
        }

        public async Task<List<BloodRequestDto>> GetAllBloodRequest(HttpRequest request)
        {
            return await _context.BloodRequests.Select(x => new BloodRequestDto
            {
                RequestId = x.RequestId,
                UserId = x.UserId,
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
                CreatedTime = x.CreatedTime,
                MedicalReportUrl = x.MedicalReport != null ? $"{request.Scheme}://{request.Host}{x.MedicalReport}" : null
            }).ToListAsync();
        }

        public async Task<BloodRequestDto?> GetBloodRequestById(int id, HttpRequest request)
        {
            return await _context.BloodRequests.Where(x => x.RequestId == id)
                .Select(x => new BloodRequestDto
                {
                    RequestId = x.RequestId,
                    UserId = x.UserId,
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
                    CreatedTime = x.CreatedTime,
                    MedicalReportUrl = x.MedicalReport != null ? $"{request.Scheme}://{request.Host}{x.MedicalReport}" : null
                }).FirstOrDefaultAsync();
        }

        public async Task<BloodRequestDto?> CreateBloodRequest(BloodRequestDto dto, HttpRequest request)
        {
            var existing = await _context.BloodRequests
                .FirstOrDefaultAsync(x => x.UserId == dto.UserId && x.Status == 0);
            if (existing != null)
                return null;

            string? uploadedFileName = null;
            if (dto.MedicalFile != null)
            {
                var ext = Path.GetExtension(dto.MedicalFile.FileName).ToLowerInvariant();
                if (!new[] { ".jpg", ".jpeg", ".png", ".gif", ".pdf" }.Contains(ext))
                    return null;

                var folder = Path.Combine(_env.WebRootPath, "uploads");
                if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);

                uploadedFileName = $"{Guid.NewGuid()}{ext}";
                var path = Path.Combine(folder, uploadedFileName);
                using var stream = new FileStream(path, FileMode.Create);
                await dto.MedicalFile.CopyToAsync(stream);
            }

            var vnTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow,
                TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time"));

            var entity = new BloodRequests
            {
                UserId = dto.UserId,
                PatientId = dto.PatientId,
                PatientName = dto.PatientName,
                Age = dto.Age,
                Gender = dto.Gender,
                Relationship = dto.Relationship,
                BloodGroup = dto.BloodGroup,
                RhType = dto.RhType,
                ComponentId = dto.ComponentId,
                Quantity = dto.Quantity,
                Reason = dto.Reason,
                CreatedTime = vnTime,
                MedicalReport = uploadedFileName != null ? $"/uploads/{uploadedFileName}" : null,
            };

            if (dto.Relationship == "Bác sĩ phụ trách")
            {
                var user = await _context.Users.FindAsync(dto.UserId);
                if (user != null)
                {
                    entity.DoctorName = user.Name;
                    entity.DoctorPhone = user.Phone;
                    entity.Status = 1;
                    entity.FacilityName = "Ánh Dương";
                }
            }
            else
            {
                entity.Status = 0;
                entity.DoctorName = dto.DoctorName;
                entity.DoctorPhone = dto.DoctorPhone;
                entity.FacilityName = dto.FacilityName;
            }

            _context.BloodRequests.Add(entity);
            await _context.SaveChangesAsync();

            await _logger.NotiLog(entity.UserId, "Yêu cầu máu", "Tạo yêu cầu", "Create");

            dto.RequestId = entity.RequestId;
            dto.Status = entity.Status;
            dto.CreatedTime = entity.CreatedTime;
            dto.MedicalReportUrl = entity.MedicalReport;

            return dto;
        }

        public async Task<bool> UpdateBloodRequest(int id, BloodRequestDto dto)
        {
            var req = await _context.BloodRequests.FindAsync(id);
            if (req == null || req.Status == 2 || req.Status == 3 || req.Status == 4) return false;

            req.PatientId = dto.PatientId;
            req.PatientName = dto.PatientName;
            req.Age = dto.Age;
            req.Gender = dto.Gender;
            req.Relationship = dto.Relationship;
            req.FacilityName = dto.FacilityName;
            req.DoctorName = dto.DoctorName;
            req.DoctorPhone = dto.DoctorPhone;
            req.BloodGroup = dto.BloodGroup;
            req.RhType = dto.RhType;
            req.ComponentId = dto.ComponentId;
            req.Quantity = dto.Quantity;
            req.Reason = dto.Reason;

            _context.BloodRequests.Update(req);
            await _context.SaveChangesAsync();

            await _logger.NotiLog(req.UserId, "Yêu cầu máu", "Sửa yêu cầu", "Update");
            return true;
        }

        public async Task<bool> PatchStatusBloodRequest(int id, byte status)
        {
            var req = await _context.BloodRequests.FindAsync(id);
            if (req == null) return false;

            req.Status = status;
            _context.BloodRequests.Update(req);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteBloodRequest(int id)
        {
            var req = await _context.BloodRequests.FindAsync(id);
            if (req == null) 
                return false;

            req.Status = 4;
            _context.BloodRequests.Update(req);
            await _context.SaveChangesAsync();

            await _logger.NotiLog(req.UserId, "Yêu cầu máu", "Xóa yêu cầu", "Delete");
            return true;
        }
    }

}
