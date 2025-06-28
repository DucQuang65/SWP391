using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Hien_mau.Data;
using Hien_mau.Models;
using Hien_mau.Dto;
using System;
using System.Linq;
using System.Threading.Tasks;

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

    // GET: api/users/{userId}
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
                u.BloodGroup,
                u.RhType,
                u.Weight,
                u.Height
            })
            .FirstOrDefaultAsync();

        if (user == null)
        {
            return NotFound(new { error = "User Not Found" });
        }

        return Ok(user);
    }

    // GET: api/blood-donation-submissions
    [HttpGet("blood-donation-submissions")]
    public async Task<IActionResult> GetAllSubmissions()
    {
        var submissions = await _context.Appointments
            .Include(a => a.User)
            .Select(a => new AppointmentDetailDto
            {
                Name = a.User.Name,
                Email = a.User.Email,
                Phone = a.User.Phone,
                BloodGroup = a.User.BloodGroup,
                Address = a.User.Address,
                DateOfBirth = a.User.DateOfBirth,
                Gender = a.User.Gender,
                RhType = a.User.RhType,
                Weight = (float)a.User.Weight,
                Height = (float)a.User.Height,
                AppointmentId = a.AppointmentId,
                AppointmentDate =a.AppointmentDate,
                UserId = a.UserId,
                CreatedAt = a.CreatedAt,
                TimeSlot = a.TimeSlot,
                LastDonationDate = a.LastDonationDate
            })
            .ToListAsync();

        return Ok(submissions);
    }

    // GET: api/blood-donation-submissions/{appointmentId}
    [HttpGet("blood-donation-submissions/{appointmentId}")]
    public async Task<IActionResult> GetSubmissionById(int appointmentId)
    {
        var submission = await _context.Appointments
            .Include(a => a.User)
            .Where(a => a.AppointmentId == appointmentId)
            .Select(a => new AppointmentDetailDto
            {
                Name = a.User.Name,
                Email = a.User.Email,
                Phone = a.User.Phone,
                BloodGroup = a.User.BloodGroup,
                Address = a.User.Address,
                DateOfBirth = a.User.DateOfBirth,
                Gender = a.User.Gender,
                RhType = a.User.RhType,
                Weight = (float)a.User.Weight,
                Height = (float)a.User.Height,
                AppointmentId = a.AppointmentId,
                AppointmentDate =a.AppointmentDate,
                UserId = a.UserId,
                CreatedAt = a.CreatedAt,
                TimeSlot = a.TimeSlot,
                LastDonationDate = a.LastDonationDate
            })
            .FirstOrDefaultAsync();

        if (submission == null)
        {
            return NotFound(new { error = "Blood donation submission not found!" });
        }

        return Ok(submission);
    }

    // POST: api/blood-donation-submissions
    [HttpPost("blood-donation-submissions")]
    public async Task<IActionResult> CreateSubmission([FromBody] BloodDonationSubmissionDto request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new { error = "Invalid data" });
        }

        // Kiểm tra TimeSlot hợp lệ
        var validTimeSlots = new[] { "Sáng (7:00-12:00)", "Chiều (13:00-17:00)" };
        if (!validTimeSlots.Contains(request.TimeSlot))
        {
            return BadRequest(new { error = "Invalid time slot. It must be either 'Sáng (7:00-12:00)' or 'Chiều (13:00-17:00)'." });
        }

        // Kiểm tra LastDonationDate không được là ngày tương lai
        if (request.LastDonationDate.HasValue && request.LastDonationDate.Value > DateTime.Now)
        {
            return BadRequest(new { error = "LastDonationDate cannot be in the future." });
        }

        // Kiểm tra user tồn tại
        var user = await _context.Users.FindAsync(request.UserId);
        if (user == null)
        {
            return NotFound(new { error = "User does not exist" });
        }

        // Cập nhật Weight và Height cho User
        user.Weight = request.Weight;
        user.Height = request.Height;

        // Tạo appointment
        var appointment = new Appointment
        {
            UserId = request.UserId,
            AppointmentDate = request.RequestedDonationDate,
            TimeSlot = request.TimeSlot,
            LastDonationDate = request.LastDonationDate,
            CreatedAt = DateTime.Now
        };

        _context.Appointments.Add(appointment);
        await _context.SaveChangesAsync();

        // Trả về response
        var response = new
        {
            AppointmentId = appointment.AppointmentId,
            UserId = appointment.UserId,
            RequestedDonationDate = appointment.AppointmentDate,
            TimeSlot = appointment.TimeSlot,
            Weight = user.Weight,
            Height = user.Height,
            LastDonationDate = appointment.LastDonationDate,
            CreatedAt = appointment.CreatedAt
        };

        return CreatedAtAction(nameof(GetSubmissionById), new { appointmentId = appointment.AppointmentId }, response);
    }

    // DELETE: api/blood-donation-submissions/{appointmentId}
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