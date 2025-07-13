using Hien_mau.Data;
using Hien_mau.Dto;

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

    public AppointmentController(Hien_mauContext context)
    {
        _context = context;
    }

  
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AppointmentDTO>>> GetAppointments()
    {
        var appointments = await _context.Appointments
            .Where(a => !a.Cancel)
            .Select(a => new AppointmentDTO
            {
                AppointmentId = a.AppointmentId,
                UserId = a.UserId,
                AppointmentDate = a.AppointmentDate,
                TimeSlot = a.TimeSlot,
                Status = a.Status,
                Process = a.Process,
                Notes = a.Notes,
                BloodPressure = a.BloodPressure, 
                HeartRate = a.HeartRate,
                Hemoglobin = a.Hemoglobin,
                Temperature = a.Temperature,
                DoctorId = a.DoctorId,
                Cancel = a.Cancel,
                CreatedAt = a.CreatedAt
            })
            .ToListAsync();

        return Ok(appointments);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AppointmentDTO>> GetAppointmentById(int id)
    {
        var appointment = await _context.Appointments
            .Where(a => a.AppointmentId == id && !a.Cancel)
            .Select(a => new AppointmentDTO
            {
                AppointmentId = a.AppointmentId,
                UserId = a.UserId,
                AppointmentDate = a.AppointmentDate,
                TimeSlot = a.TimeSlot,
                Status = a.Status,
                Process = a.Process,
                Notes = a.Notes,
                BloodPressure = a.BloodPressure,
                HeartRate = a.HeartRate,
                Hemoglobin = a.Hemoglobin,
                Temperature = a.Temperature,
                DoctorId = a.DoctorId,
                Cancel = a.Cancel,
                CreatedAt = a.CreatedAt
            })
            .FirstOrDefaultAsync();

        if (appointment == null) return NotFound();

        return Ok(appointment);
    }

    [HttpGet("last-donation/{userId}")]
    public async Task<ActionResult<AppointmentLastDonationDTO>> GetLastDonation(int userId)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user == null) return NotFound("User not found");

        // Lấy toàn bộ entity để kiểm tra DonationDate
        var lastDonationRecord = await _context.BloodDonationHistories
            .Include(x => x.Appointment)
            .Where(x => x.Appointment.UserId == userId && x.IsSuccess == true)
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

    [HttpPost]
    public async Task<IActionResult> CreateAppointment([FromBody] AppointmentCreateDTO dto, [FromServices] NotificationLog logger)
    {
        var user = await _context.Users.FindAsync(dto.UserId);
        if (user == null) return NotFound("User not found");

        var lastDonationRecord = await _context.BloodDonationHistories
            .Where(x => x.IsSuccess && x.Appointment != null && x.Appointment.UserId == dto.UserId)
            .OrderByDescending(x => x.DonationDate)
            .Select(x => new { x.DonationDate })
            .FirstOrDefaultAsync();


        DateTime? lastDonationFromSystem = lastDonationRecord?.DonationDate;
        DateTime? lastDonationDate = lastDonationFromSystem ?? user.SelfReportedLastDonationDate;



        if (dto.TimeSlot != "Sáng (7:00-12:00)" && dto.TimeSlot != "Chiều (13:00-17:00)")
            return BadRequest("TimeSlot must be 'Sáng (7:00-12:00)' or 'Chiều (13:00-17:00)'");
        if (dto.AppointmentDate.Date < DateTime.Today)
            return BadRequest("AppointmentDate cannot be in the past");


        if (!lastDonationFromSystem.HasValue && !user.SelfReportedLastDonationDate.HasValue)
        {
            return BadRequest("Vui lòng nhập ngày hiến máu gần nhất (tự khai) trước khi tạo lịch hẹn nếu bạn đã từng hiến máu.");
        }

        if (lastDonationDate.HasValue)
        {
            var daysSinceLastDonation = (dto.AppointmentDate - lastDonationDate.Value).TotalDays;


            var minDays = 84;

            if (daysSinceLastDonation < minDays)
            {
                return BadRequest($"Chưa đủ thời gian nghỉ sau hiến máu. Cần ít nhất {minDays} ngày kể từ {lastDonationDate.Value:dd/MM/yyyy}.");
            }
        }

     
    
       
        var appointment = new Appointments
        {
            UserId = dto.UserId,
            AppointmentDate = dto.AppointmentDate,
            TimeSlot = dto.TimeSlot,
           
            Status = dto.Status,
            Process = dto.Process,
            CreatedAt = DateTime.Now
        };

        _context.Appointments.Add(appointment);
        await _context.SaveChangesAsync();
       
        var remindTime = dto.AppointmentDate.AddDays(-1).Date.AddHours(8);
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

        await logger.NotiLog(dto.UserId, "Appointment", $"Tạo hẹn:", "Create");

        return Ok(new { Message = "Appointment created successfully", AppointmentId = appointment.AppointmentId });
    }


    [HttpPost("doctor-update/{id}")]
    public async Task<IActionResult> DoctorUpdate(int id, [FromBody] DoctorUpdateDTO dto, [FromServices] NotificationLog logger)
    {
        var appointment = await _context.Appointments.FindAsync(id);
        if (appointment == null) return NotFound();

        appointment.Notes = dto.Notes;
        appointment.BloodPressure = dto.BloodPressure; 
        appointment.HeartRate = dto.HeartRate;
        appointment.Hemoglobin = dto.Hemoglobin;
        appointment.Temperature = dto.Temperature;
        appointment.DoctorId = dto.DoctorId;
        appointment.Status = dto.Status; 
        appointment.Process = dto.Process;

        await _context.SaveChangesAsync();
        await logger.NotiLog(dto.AppointmentId, "Appointment", $"Cập nhật hẹn:", "Update");
        return Ok(new AppointmentDTO
        {
            AppointmentId = appointment.AppointmentId,
            UserId = appointment.UserId,
            AppointmentDate = appointment.AppointmentDate,
            TimeSlot = appointment.TimeSlot,
            Status = appointment.Status,
            Process = appointment.Process,
            Notes = appointment.Notes,
            BloodPressure = appointment.BloodPressure, 
            HeartRate = appointment.HeartRate,
            Hemoglobin = appointment.Hemoglobin,
            Temperature = appointment.Temperature,
            DoctorId = appointment.DoctorId,
            Cancel = appointment.Cancel,
            CreatedAt = appointment.CreatedAt
        });
    }

   
    [HttpPatch("{id}/status/{status}")]
    public async Task<IActionResult> UpdateStatus(int id, bool status, [FromServices] NotificationLog logger)
    {
        var appointment = await _context.Appointments.FindAsync(id);
        if (appointment == null) return NotFound();

        //if (status > 2) return BadRequest("Invalid status");

        appointment.Status = status;
        await _context.SaveChangesAsync();
        await logger.NotiLog(id, "Appointment", $"Cập nhật trạng thái:", "Update");
        return Ok(new AppointmentDTO
        {
            AppointmentId = appointment.AppointmentId,
            UserId = appointment.UserId,
            AppointmentDate = appointment.AppointmentDate,
            TimeSlot = appointment.TimeSlot,
            Status = appointment.Status,
            Process = appointment.Process,
            Notes = appointment.Notes,
            BloodPressure = appointment.BloodPressure, 
            HeartRate = appointment.HeartRate,
            Hemoglobin = appointment.Hemoglobin,
            Temperature = appointment.Temperature,
            DoctorId = appointment.DoctorId,
            Cancel = appointment.Cancel,
            CreatedAt = appointment.CreatedAt
        });
    }

    [HttpPatch("{id}/process/{process}")]
    public async Task<IActionResult> UpdateProcess(int id, byte process, [FromServices] NotificationLog logger)
    {
        var appointment = await _context.Appointments.FindAsync(id);
        if (appointment == null) return NotFound();

        if (process > 5) return BadRequest("Invalid status");

        appointment.Process = process;
        await _context.SaveChangesAsync();
        await logger.NotiLog(id, "Appointment", $"Cập nhật quy trình:", "Update");
        return Ok(new AppointmentDTO
        {
            AppointmentId = appointment.AppointmentId,
            UserId = appointment.UserId,
            AppointmentDate = appointment.AppointmentDate,
            TimeSlot = appointment.TimeSlot,
            Status = appointment.Status,
            Process = appointment.Process,
            Notes = appointment.Notes,
            BloodPressure = appointment.BloodPressure,
            HeartRate = appointment.HeartRate,
            Hemoglobin = appointment.Hemoglobin,
            Temperature = appointment.Temperature,
            DoctorId = appointment.DoctorId,
            Cancel = appointment.Cancel,
            CreatedAt = appointment.CreatedAt
        });
    }

    [HttpPatch("{id}/cancel")]
    public async Task<IActionResult> CancelAppointment(int id)
    {
        var appointment = await _context.Appointments.FindAsync(id);
        if (appointment == null) return NotFound();

        appointment.Cancel = true;
        await _context.SaveChangesAsync();

        return Ok(new { message = "Appointment cancelled" });
    }
}