using Hien_mau.Data;
using Hien_mau.Dto;

using Hien_mau.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        // Kiểm tra lần hiến gần nhất từ BloodDonationHistories
        var lastDonationFromSystem = await _context.BloodDonationHistories
            .Where(x => x.Appointment.UserId == userId && x.IsSuccess == true)
            .OrderByDescending(x => x.DonationDate)
            .Select(x => x.DonationDate)
            .FirstOrDefaultAsync();

        if (lastDonationFromSystem != default)
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
    public async Task<IActionResult> CreateAppointment([FromBody] AppointmentCreateDTO dto)
    {
        var user = await _context.Users.FindAsync(dto.UserId);
        if (user == null) return NotFound("User not found");

        // Tính ngày hiến máu gần nhất
        var lastDonationFromSystem = await _context.BloodDonationHistories
            .Where(x => x.Appointment.UserId == dto.UserId && x.IsSuccess == true)
            .OrderByDescending(x => x.DonationDate)
            .Select(x => x.DonationDate)
            .FirstOrDefaultAsync();

        var lastDonationDate = lastDonationFromSystem != default
            ? lastDonationFromSystem
            : user.SelfReportedLastDonationDate;

        if (lastDonationDate.HasValue)
        {
            var daysSinceLastDonation = (dto.AppointmentDate - lastDonationDate.Value).TotalDays;


            var minDays = 84; // Thời gian tối thiểu giữa các lần hiến máu (12 tuần = 84 ngày)

            if (daysSinceLastDonation < minDays)
            {
                return BadRequest($"Chưa đủ thời gian nghỉ sau hiến máu. Cần ít nhất {minDays} ngày kể từ {lastDonationDate.Value:dd/MM/yyyy}.");
            }
        }

        
        if (dto.TimeSlot != "Sáng (7:00-12:00)" && dto.TimeSlot != "Chiều (13:00-17:00)")
            return BadRequest("TimeSlot must be 'Sáng (7:00-12:00)' or 'Chiều (13:00-17:00)'");

        if (dto.AppointmentDate.Date < DateTime.Today)
            return BadRequest("AppointmentDate cannot be in the past");

        var appointment = new Appointments
        {
            UserId = dto.UserId,
            AppointmentDate = dto.AppointmentDate,
            TimeSlot = dto.TimeSlot,

            Status = 0, 
            CreatedAt = DateTime.Now
        };

        _context.Appointments.Add(appointment);
        await _context.SaveChangesAsync();

        return Ok(new { Message = "Appointment created successfully", AppointmentId = appointment.AppointmentId });
    }

    
    [HttpPost("doctor-update/{id}")]
    public async Task<IActionResult> DoctorUpdate(int id, [FromBody] DoctorUpdateDTO dto)
    {
        var appointment = await _context.Appointments.FindAsync(id);
        if (appointment == null) return NotFound();

        appointment.Notes = dto.Notes;
        appointment.BloodPressure = dto.BloodPressure; 
        appointment.HeartRate = dto.HeartRate;
        appointment.Hemoglobin = dto.Hemoglobin;
        appointment.Temperature = dto.Temperature;
        appointment.DoctorId = dto.DoctorId;
        appointment.Status = 2; 

        await _context.SaveChangesAsync();
        return Ok(new AppointmentDTO
        {
            AppointmentId = appointment.AppointmentId,
            UserId = appointment.UserId,
            AppointmentDate = appointment.AppointmentDate,
            TimeSlot = appointment.TimeSlot,
            Status = appointment.Status,
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
    public async Task<IActionResult> UpdateStatus(int id, byte status)
    {
        var appointment = await _context.Appointments.FindAsync(id);
        if (appointment == null) return NotFound();

        if (status > 2) return BadRequest("Invalid status");

        appointment.Status = status;
        await _context.SaveChangesAsync();

        return Ok(new AppointmentDTO
        {
            AppointmentId = appointment.AppointmentId,
            UserId = appointment.UserId,
            AppointmentDate = appointment.AppointmentDate,
            TimeSlot = appointment.TimeSlot,
            Status = appointment.Status,
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