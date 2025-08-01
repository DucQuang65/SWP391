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
                Note = x.Note,
                CreatedTime = x.CreatedTime,
                MedicalReportUrl = x.MedicalReport != null ? $"{request.Scheme}://{request.Host}{x.MedicalReport}" : null,
                ApprovedByDoctorId = x.ApprovedByDoctorId,
                ApprovedByDoctorName = x.ApprovedByDoctorName,
                ApprovedTime = x.ApprovedTime
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
                    Note = x.Note,
                    CreatedTime = x.CreatedTime,
                    MedicalReportUrl = x.MedicalReport != null ? $"{request.Scheme}://{request.Host}{x.MedicalReport}" : null,
                    ApprovedByDoctorId = x.ApprovedByDoctorId,
                    ApprovedByDoctorName = x.ApprovedByDoctorName,
                    ApprovedTime = x.ApprovedTime
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

            var bloodRequest = new BloodRequests
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
                Note = dto.Note,
                CreatedTime = vnTime,
                MedicalReport = uploadedFileName != null ? $"/uploads/{uploadedFileName}" : null,
            };

            if (dto.Relationship == "Bác sĩ phụ trách")
            {
                var user = await _context.Users.FindAsync(dto.UserId);
                if (user != null)
                {
                    bloodRequest.DoctorName = user.Name;
                    bloodRequest.DoctorPhone = user.Phone;
                    bloodRequest.Status = 1;
                    bloodRequest.FacilityName = "Ánh Dương";
                }
            }
            else
            {
                bloodRequest.Status = 0;
                bloodRequest.DoctorName = dto.DoctorName;
                bloodRequest.DoctorPhone = dto.DoctorPhone;
                bloodRequest.FacilityName = dto.FacilityName;
            }

            _context.BloodRequests.Add(bloodRequest);
            await _context.SaveChangesAsync();

            await _logger.NotiLog(bloodRequest.UserId, "Yêu cầu máu", "Tạo yêu cầu", "Create");

            dto.RequestId = bloodRequest.RequestId;
            dto.Status = bloodRequest.Status;
            dto.CreatedTime = bloodRequest.CreatedTime;
            dto.MedicalReportUrl = bloodRequest.MedicalReport;

            return dto;
        }

        public async Task<bool> UpdateBloodRequest(int id, BloodRequestDto dto)
        {
            var bloodRequest = await _context.BloodRequests.FindAsync(id);
            if (bloodRequest == null || bloodRequest.Status == 2 || bloodRequest.Status == 3 || bloodRequest.Status == 4) 
                return false;

            bloodRequest.PatientId = dto.PatientId;
            bloodRequest.PatientName = dto.PatientName;
            bloodRequest.Age = dto.Age;
            bloodRequest.Gender = dto.Gender;
            bloodRequest.Relationship = dto.Relationship;
            bloodRequest.FacilityName = dto.FacilityName;
            bloodRequest.DoctorName = dto.DoctorName;
            bloodRequest.DoctorPhone = dto.DoctorPhone;
            bloodRequest.BloodGroup = dto.BloodGroup;
            bloodRequest.RhType = dto.RhType;
            bloodRequest.ComponentId = dto.ComponentId;
            bloodRequest.Quantity = dto.Quantity;
            bloodRequest.Reason = dto.Reason;
            bloodRequest.Note = dto.Note;

            _context.BloodRequests.Update(bloodRequest);
            await _context.SaveChangesAsync();

            await _logger.NotiLog(bloodRequest.UserId, "Yêu cầu máu", "Sửa yêu cầu", "Update");
            return true;
        }

        public async Task<bool> PatchStatusBloodRequest(int id, byte status, string? note = null, int? approvedByDoctorId = null)
        {
            var bloodRequest = await _context.BloodRequests.FindAsync(id);
            if (bloodRequest == null) 
                return false;

            bloodRequest.Status = status;

            var vnTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow,
                TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time"));

            if (approvedByDoctorId.HasValue)
            {
                var approver = await _context.Users.FindAsync(approvedByDoctorId.Value);
                if (approver != null)
                {
                    bloodRequest.ApprovedByDoctorId = approver.UserId;
                    bloodRequest.ApprovedByDoctorName = approver.Name;
                    bloodRequest.ApprovedTime = vnTime;
                }
            }

            bloodRequest.Note = (status == 3 && !string.IsNullOrWhiteSpace(note)) ? note : null;

            _context.BloodRequests.Update(bloodRequest);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteBloodRequest(int id)
        {
            var bloodRequest = await _context.BloodRequests.FindAsync(id);
            if (bloodRequest == null) 
                return false;

            bloodRequest.Status = 4;
            _context.BloodRequests.Update(bloodRequest);
            await _context.SaveChangesAsync();

            await _logger.NotiLog(bloodRequest.UserId, "Yêu cầu máu", "Xóa yêu cầu", "Delete");
            return true;
        }
    }

}
