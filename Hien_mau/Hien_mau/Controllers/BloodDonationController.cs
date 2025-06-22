using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Hien_mau.Data;
using Hien_mau.Models;
using System.Linq;
using System.Threading.Tasks;
using Hien_mau.Dto;

namespace Hien_mau.Controllers;

[Route("api")]
[ApiController]
public class BloodDonationController : ControllerBase
{
    private readonly Hien_mauContext _context;

    public BloodDonationController(Hien_mauContext context)
    {
        _context = context;
    }

  
    [HttpGet("users/{userId}")]
    public async Task<IActionResult> GetUser(int userId)
    {
        var user = await _context.Users
            .Where(u => u.UserId == userId && u.Status == 1)
            .Select(u => new
            {
                u.Name,
                u.Email,
                u.Phone,
                u.DateOfBirth,
                u.Gender,
                u.Address,
                u.Weight,
                u.Height,
                u.BloodGroup,
                u.RhType
            })
            .FirstOrDefaultAsync();

        if (user == null)
        {
            return NotFound(new { error = "User Not Found" });
        }

        return Ok(user);
    }


    [HttpGet("blood-donation-submissions")]
    public async Task<IActionResult> GetAllSubmissions()
    {
        var submissions = await _context.Appointments
            .Join(_context.Users,
                a => a.UserId,
                u => u.UserId,
                (a, u) => new
                {
                    AppointmentId = a.AppointmentId,
                    UserId = a.UserId,
                    Name = u.Name,
                    Email = u.Email,
                    Phone = u.Phone,
                    DateOfBirth = u.DateOfBirth,
                    Gender = u.Gender,
                    Address = u.Address,
                    Weight = u.Weight,
                    Height = u.Height,
                    BloodGroup = u.BloodGroup,
                    RhType = u.RhType,
                    RequestedDonationDate = a.AppointmentDate,
                    TimeSlot = a.TimeSlot,
                    Notes = a.Notes,
                    CreatedAt = a.CreatedAt
                })
            .ToListAsync();

        return Ok(submissions);
    }


    [HttpGet("blood-donation-submissions/{AppointmentId}")]
    public async Task<IActionResult> GetSubmissionById(int AppointmentId)
    {
        var submission = await _context.Appointments
            .Join(_context.Users,
                a => a.UserId,
                u => u.UserId,
                (a, u) => new
                {
                    AppointmentId = a.AppointmentId,
                    UserId = a.UserId,
                    Name = u.Name,
                    Email = u.Email,
                    Phone = u.Phone,
                    DateOfBirth = u.DateOfBirth,
                    Gender = u.Gender,
                    Address = u.Address,
                    Weight = u.Weight,
                    Height = u.Height,
                    BloodGroup = u.BloodGroup,
                    RhType = u.RhType,
                    RequestedDonationDate = a.AppointmentDate,
                    TimeSlot = a.TimeSlot,
                    Notes = a.Notes,
                    CreatedAt = a.CreatedAt
                })
            .FirstOrDefaultAsync(a => a.AppointmentId == AppointmentId);

        if (submission == null)
        {
            return NotFound(new { error = "Blood donation submission not found!" });
        }

        return Ok(submission);
    }

 
    [HttpPost("blood-donation-submissions")]
    public async Task<IActionResult> CreateSubmission([FromBody] BloodDonationSubmissionDto request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new { error = "Invalid data" });
        }

        // Kiểm tra timeSlot hợp lệ
        if (request.TimeSlot != "Sáng (7:00-12:00)" && request.TimeSlot != "Chiều (13:00-17:00)")
        {
            return BadRequest(new { error = "Invalid time slot. It must be either 'Sáng (7:00-12:00)' or 'Chiều (13:00-17:00)'." });
        }

        // Cập nhật Weight và Height cho user
        var user = await _context.Users.FindAsync(request.UserId);
        if (user == null)
        {
            return NotFound(new { error = "User does not exist" });
        }

        user.Weight = request.Weight;
        user.Height = request.Height;

        // Lưu appointment với Notes dựa trên hasDonated
        var appointment = new Appointment
        {
            UserId = request.UserId,
            AppointmentDate = request.RequestedDonationDate,
            TimeSlot = request.TimeSlot,
            Notes = request.HasDonated ? "Đã từng hiến máu" : "Chưa từng hiến máu",
            CreatedAt = DateTime.Now
        };

        _context.Appointments.Add(appointment);
        await _context.SaveChangesAsync();

        return Ok(new
        {
            AppointmentId = appointment.AppointmentId,
            userId = appointment.UserId,
            requestedDonationDate = appointment.AppointmentDate,
            timeSlot = appointment.TimeSlot,
            createdAt = appointment.CreatedAt
        });
    }
    // DELETE /api/blood-donation-submissions/{appointmentId}
    [HttpDelete("blood-donation-submissions/{appointmentId}")]
    public async Task<IActionResult> DeleteSubmission(int appointmentId)
    {
        var appointment = await _context.Appointments.FindAsync(appointmentId);
        if (appointment == null)
        {
            return NotFound(new { error = "Blood donation submission not found!" });
        }

        _context.Appointments.Remove(appointment);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Deleted successfully" });
    }
}