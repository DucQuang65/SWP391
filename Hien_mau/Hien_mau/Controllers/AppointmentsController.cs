
using Hien_mau.Data;
using Hien_mau.Dto;
using Hien_mau.Interface;
using Hien_mau.Models;
using Hien_mau.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Hien_mau.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AppointmentController : ControllerBase
{
    private readonly Hien_mauContext _context;
    private readonly ISendEmail _sendEmail;

    public AppointmentController(Hien_mauContext context, ISendEmail sendEmail)
    {
        _context = context;
        _sendEmail = sendEmail;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AppointmentDTO>>> GetAppointments()
    {
        var appointments = await _context.Appointments
            .Select(a => new AppointmentDTO
            {
                AppointmentId = a.AppointmentID,
                UserId = a.UserID,
                AppointmentDate = a.AppointmentDate,
                TimeSlot = a.TimeSlot,
                Status = a.Status,
                Process = a.Process,
                Notes = a.Notes,
                BloodPressure = a.BloodPressure,
                HeartRate = a.HeartRate,
                Hemoglobin = a.Hemoglobin,
                Temperature = a.Temperature,
                DoctorId1 = a.DoctorID1,
                DoctorId2 = a.DoctorID2,
                Cancel = a.Cancel,
                CreatedAt = a.CreatedAt,
                WeightAppointment = a.WeightAppointment,
                HeightAppointment = a.HeightAppointment,
                DonationCapacity = a.DonationCapacity,
                DonationDate = a.DonationDate,
                BloodGroup = a.BloodGroup,
                RhType = a.RhType
            })
            .ToListAsync();

        return Ok(appointments);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AppointmentDTO>> GetAppointmentById(int id)
    {
        var appointment = await _context.Appointments
            .Where(a => a.AppointmentID == id)
            .Select(a => new AppointmentDTO
            {
                AppointmentId = a.AppointmentID,
                UserId = a.UserID,
                AppointmentDate = a.AppointmentDate,
                TimeSlot = a.TimeSlot,
                Status = a.Status,
                Process = a.Process,
                Notes = a.Notes,
                BloodPressure = a.BloodPressure,
                HeartRate = a.HeartRate,
                Hemoglobin = a.Hemoglobin,
                Temperature = a.Temperature,
                DoctorId1 = a.DoctorID1,
                DoctorId2 = a.DoctorID2,
                Cancel = a.Cancel,
                CreatedAt = a.CreatedAt,
                WeightAppointment = a.WeightAppointment,
                HeightAppointment = a.HeightAppointment,
                DonationCapacity = a.DonationCapacity,
                DonationDate = a.DonationDate,
                BloodGroup = a.BloodGroup,
                RhType = a.RhType
            })
            .FirstOrDefaultAsync();

        if (appointment == null) return NotFound();

        return Ok(appointment);
    }

    [HttpGet("donation-histories")]
    public async Task<ActionResult<IEnumerable<BloodDonationHistoryDTO>>> GetDonationHistories()
    {
        var donations = await _context.Appointments
            .Where(a => (a.Process == 2 && a.Status == true) || a.Process == 3 || a.Process == 4)
            .Select(a => new BloodDonationHistoryDTO
            {
                DonationId = a.AppointmentID,
                AppointmentId = a.AppointmentID,
                UserId = a.UserID,
                DonationDate = a.DonationDate,
                BloodGroup = a.BloodGroup,
                RhType = a.RhType,
                DoctorId = a.DoctorID2,
                Notes = a.Notes,
                CreatedAt = a.CreatedAt
            })
            .ToListAsync();

        return Ok(donations);
    }

    [HttpGet("statistics/donors")]
    public async Task<IActionResult> GetDonorStatistics()
    {
        var stats = await _context.Appointments
            .Where(a => a.Status == true && a.Process == 4)
            .Join(_context.Users,
                  a => a.UserID,
                  u => u.UserId,
                  (a, u) => new { a, u })
            .GroupBy(x => new { x.u.BloodGroup, x.u.RhType })
            .Select(g => new
            {
                BloodGroup = g.Key.BloodGroup,
                RhType = g.Key.RhType,
                NumDonors = g.Select(x => x.a.UserID).Distinct().Count(),
                TotalDonations = g.Count()
            })
            .ToListAsync();

        return Ok(stats);
    }


    [HttpGet("last-donation/{userId}")]
    public async Task<ActionResult<AppointmentLastDonationDTO>> GetLastDonation(int userId)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user == null) return NotFound("User not found");

        var lastDonationRecord = await _context.Appointments
            .Where(x => x.UserID == userId &&
                        ((x.Process == 2 && x.Status == true) || x.Process == 3 || x.Process == 4))
            .OrderByDescending(x => x.DonationDate)
            .FirstOrDefaultAsync();

        DateTime? lastDonationFromSystem = lastDonationRecord?.DonationDate;

        if (lastDonationFromSystem.HasValue)
        {
            return Ok(new AppointmentLastDonationDTO
            {
                HasDonationHistory = true,
                LastDonationDate = lastDonationFromSystem,
                IsEditable = false
            });
        }

        return Ok(new AppointmentLastDonationDTO
        {
            HasDonationHistory = false,
            LastDonationDate = user.SelfReportedLastDonationDate,
            IsEditable = true
        });
    }

    [HttpPost("send-reminders")]
    public async Task<IActionResult> SendReminders()
    {
        await _sendEmail.SendAppointmentReminders();
        return Ok("Email nhắc nhở đã được gửi cho các cuộc hẹn ngày mai.");
    }

    [HttpPost("send-donation-call")]
    public async Task<IActionResult> SendEmergencyCall()
    {
        await _sendEmail.SendDonationBloodCall();
        return Ok("Đã gửi email kêu gọi hiến máu.");
    }

    [HttpPost]
    public async Task<IActionResult> CreateAppointment([FromBody] AppointmentCreateDTO dto, [FromServices] NotificationLog logger)
    {
        var user = await _context.Users.FindAsync(dto.UserId);
        if (user == null) return NotFound("User not found");

        var lastDonationResult = await GetLastDonation(dto.UserId);
        if (lastDonationResult.Result is NotFoundObjectResult) // Sửa: Kiểm tra NotFoundObjectResult thay vì NotFoundResult
        {
            return NotFound("User not found or no donation history");
        }

        var okResult = lastDonationResult.Result as OkObjectResult; // Sửa: Lấy OkObjectResult từ Result
        if (okResult == null)
        {
            return BadRequest("Unexpected response from GetLastDonation");
        }

        var lastDonationDto = okResult.Value as AppointmentLastDonationDTO; // Sửa: Lấy Value từ OkObjectResult
        if (lastDonationDto == null)
        {
            return BadRequest("Invalid response format from GetLastDonation");
        }

        DateTime? lastDonationDate = lastDonationDto.LastDonationDate;

        if (dto.TimeSlot != "Sáng (7:00-12:00)" && dto.TimeSlot != "Chiều (13:00-17:00)")
            return BadRequest("Invalid TimeSlot");
        if (dto.AppointmentDate.Date < DateTime.Today)
            return BadRequest("AppointmentDate cannot be in the past");

        if (lastDonationDate.HasValue)
        {
            var daysSince = (dto.AppointmentDate - lastDonationDate.Value).TotalDays;
            if (daysSince < 84)
                return BadRequest($"Chưa đủ 84 ngày từ lần hiến cuối ({lastDonationDate.Value:dd/MM/yyyy}).");
        }
        else if (lastDonationDto.HasDonationHistory == false && user.SelfReportedLastDonationDate == null)
        {
            return BadRequest("Vui lòng khai báo lần hiến máu cuối (nếu có) trước khi tạo lịch.");
        }

        var appointment = new Appointments
        {
            UserID = dto.UserId,
            AppointmentDate = dto.AppointmentDate,
            TimeSlot = dto.TimeSlot,
            Status = dto.Status,
            Process = dto.Process,
            CreatedAt = DateTime.Now,
            DonationDate = dto.AppointmentDate
        };

        _context.Appointments.Add(appointment);
        await _context.SaveChangesAsync();
        await logger.NotiLog(dto.UserId, "Appointment", $"Lịch hẹn created", "Create");

        var remindTime = dto.AppointmentDate.AddDays(-1).Date.AddHours(7);
        var reminder = new Reminder
        {
            UserId = dto.UserId,
            Type = "BloodDonation",
            Message = $"Nhắc bạn chuẩn bị hiến máu vào ngày {dto.AppointmentDate:dd/MM/yyyy} ({dto.TimeSlot})",
            RemindAt = remindTime,
            CreatedAt = DateTime.Now,
            IsDisabled = false,
            IsSent = false
        };
        _context.Reminders.Add(reminder);
        await _context.SaveChangesAsync();

        return Ok(new { Message = "Created", AppointmentId = appointment.AppointmentID });
    }

    [HttpPut("doctor-health-check/{id}")]
    public async Task<IActionResult> DoctorHealthCheckUpdate(int id, [FromBody] DoctorHealthCheckDTO dto, [FromServices] NotificationLog logger)
    {
        var appointment = await _context.Appointments.FindAsync(id);
        if (appointment == null) return NotFound();

        appointment.Notes = dto.Notes;
        appointment.BloodPressure = dto.BloodPressure;
        appointment.HeartRate = dto.HeartRate;
        appointment.Hemoglobin = dto.Hemoglobin;
        appointment.Temperature = dto.Temperature;
        appointment.WeightAppointment = dto.WeightAppointment;
        appointment.HeightAppointment = dto.HeightAppointment;
        appointment.DonationCapacity = dto.DonationCapacity;
        appointment.DoctorID1 = dto.DoctorId;
        appointment.Status = dto.Status;
        appointment.Process = dto.Process;

        await _context.SaveChangesAsync();
        await logger.NotiLog(appointment.UserID, "Appointment", $"Khám sức khỏe updated", "Update");

        return Ok(new AppointmentDTO
        {
            AppointmentId = appointment.AppointmentID,
            UserId = appointment.UserID,
            AppointmentDate = appointment.AppointmentDate,
            TimeSlot = appointment.TimeSlot,
            Status = appointment.Status,
            Process = appointment.Process,
            Notes = appointment.Notes,
            BloodPressure = appointment.BloodPressure,
            HeartRate = appointment.HeartRate,
            Hemoglobin = appointment.Hemoglobin,
            Temperature = appointment.Temperature,
            DoctorId1 = appointment.DoctorID1,
            DoctorId2 = appointment.DoctorID2,
            Cancel = appointment.Cancel,
            CreatedAt = appointment.CreatedAt,
            WeightAppointment = appointment.WeightAppointment,
            HeightAppointment = appointment.HeightAppointment,
            DonationCapacity = appointment.DonationCapacity,
            DonationDate = appointment.DonationDate,
            BloodGroup = appointment.BloodGroup,
            RhType = appointment.RhType
        });
    }

    [HttpPut("doctor-examination/{id}")]
    public async Task<IActionResult> DoctorExaminationUpdate(int id, [FromBody] DoctorExaminationDTO dto, [FromServices] NotificationLog logger)
    {
        var appointment = await _context.Appointments.FindAsync(id);
        if (appointment == null) return NotFound();

        if (!new[] { "A", "B", "AB", "O" }.Contains(dto.BloodGroup))
            return BadRequest("Invalid BloodGroup");
        if (!new[] { "Rh+", "Rh-" }.Contains(dto.RhType))
            return BadRequest("Invalid RhType");

        appointment.BloodGroup = dto.BloodGroup;
        appointment.RhType = dto.RhType;
        appointment.DonationDate = dto.DonationDate;
        appointment.DoctorID2 = dto.DoctorId;
        appointment.Process = dto.Process;

        await _context.SaveChangesAsync();
        await logger.NotiLog(appointment.UserID, "Appointment", $"Xét nghiệm updated", "Update");

        return Ok(new AppointmentDTO
        {
            AppointmentId = appointment.AppointmentID,
            UserId = appointment.UserID,
            AppointmentDate = appointment.AppointmentDate,
            TimeSlot = appointment.TimeSlot,
            Status = appointment.Status,
            Process = appointment.Process,
            Notes = appointment.Notes,
            BloodPressure = appointment.BloodPressure,
            HeartRate = appointment.HeartRate,
            Hemoglobin = appointment.Hemoglobin,
            Temperature = appointment.Temperature,
            DoctorId1 = appointment.DoctorID1,
            DoctorId2 = appointment.DoctorID2,
            Cancel = appointment.Cancel,
            CreatedAt = appointment.CreatedAt,
            WeightAppointment = appointment.WeightAppointment,
            HeightAppointment = appointment.HeightAppointment,
            DonationCapacity = appointment.DonationCapacity,
            DonationDate = appointment.DonationDate,
            BloodGroup = appointment.BloodGroup,
            RhType = appointment.RhType
        });
    }   

    [HttpPatch("{id}/status/{status}")]
    public async Task<IActionResult> UpdateStatus(int id, bool status, [FromServices] NotificationLog logger)
    {
        var appointment = await _context.Appointments.FindAsync(id);
        if (appointment == null) 
            return NotFound();

        appointment.Status = status;

        _context.Appointments.Update(appointment);
        await _context.SaveChangesAsync();
        await logger.NotiLog(appointment.UserID, "Appointment", $"Status updated to {status}", "Update");

        return Ok();
    }

    [HttpPatch("{id}/process/{process}")]
    public async Task<IActionResult> UpdateProcess(int id, byte process, [FromServices] NotificationLog logger)
    {
        var appointment = await _context.Appointments.FindAsync(id);
        if (appointment == null) return NotFound();
        if (process > 5) 
            return BadRequest("Invalid process");

        appointment.Process = process;

        await _context.SaveChangesAsync();
        await logger.NotiLog(appointment.UserID, "Appointment", $"Process updated to {process}", "Update");
        
        if (appointment.Process == 5)
        {
            await _sendEmail.SendThankYouEmail(appointment);
            return Ok("Đã gửi email cảm ơn.");
        }
        return Ok();
    }

    [HttpPatch("{id}/cancel")]
    public async Task<IActionResult> CancelAppointment(int id, [FromServices] NotificationLog logger)
    {
        var appointment = await _context.Appointments.FindAsync(id);
        if (appointment == null) return NotFound();

        appointment.Cancel = true;

        await _context.SaveChangesAsync();
        await logger.NotiLog(appointment.UserID, "Appointment", $"Bạn đã hủy lịch hẹn", "Update");

        return Ok(new { message = "Đã hủy hẹn" });
    }

    [HttpPatch("{id}/note")]
    public async Task<IActionResult> UpdateNote(int id, [FromBody] NoteUpdateDTO dto, [FromServices] NotificationLog logger)
    {
        var appointment = await _context.Appointments.FindAsync(id);
        if (appointment == null) return NotFound();

        appointment.Notes = dto.Notes;

        await _context.SaveChangesAsync();
        await logger.NotiLog(appointment.UserID, "Appointment", $"Ghi chú updated", "Update");

        return Ok(new { message = "Ghi chú đã được cập nhật" });
    }
}