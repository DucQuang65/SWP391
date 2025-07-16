using Hien_mau.Data;
using Hien_mau.Dto;
using Hien_mau.DTOs;
using Hien_mau.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hien_mau.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BloodDonationHistoryController : ControllerBase
{
    private readonly Hien_mauContext _context;

    public BloodDonationHistoryController(Hien_mauContext context)
    {
        _context = context;
    }

  
    [HttpGet]
    public async Task<ActionResult<IEnumerable<BloodDonationHistoryDTO>>> GetBloodDonationHistories()
    {
        var donations = await _context.BloodDonationHistories
            .Include(d => d.Appointment)
            .ThenInclude(a => a.User)
            .Select(d => new BloodDonationHistoryDTO
            {
                DonationId = d.DonationId,
                AppointmentId = d.AppointmentId,
                UserId = d.Appointment.UserId,
                UserName = d.Appointment.User.Name,
                DonationDate = d.DonationDate,
                BloodGroup = d.BloodGroup,
                RhType = d.RhType,
                DoctorId = d.DoctorId,
                Notes = d.Notes,
                CreatedAt = d.CreatedAt
            })
            .ToListAsync();

        return Ok(donations);
    }

    
    [HttpGet("{id}")]
    public async Task<ActionResult<BloodDonationHistoryDTO>> GetBloodDonationHistory(int id)
    {
        var donation = await _context.BloodDonationHistories
            .Include(d => d.Appointment)
            .ThenInclude(a => a.User)
            .Select(d => new BloodDonationHistoryDTO
            {
                DonationId = d.DonationId,
                AppointmentId = d.AppointmentId,
                UserId = d.Appointment.UserId,
                UserName = d.Appointment.User.Name,
                DonationDate = d.DonationDate,
                BloodGroup = d.BloodGroup,
                RhType = d.RhType,
                DoctorId = d.DoctorId,
                Notes = d.Notes,
                CreatedAt = d.CreatedAt
            })
            .FirstOrDefaultAsync(d => d.DonationId == id);

        if (donation == null) return NotFound();

        return Ok(donation);
    }

   

   
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBloodDonationHistory(int id, [FromBody] BloodDonationHistoryUpdateDTO dto)
    {
        var donation = await _context.BloodDonationHistories.FindAsync(id);
        if (donation == null)
            return NotFound();

      
        var appointment = await _context.Appointments
            .Where(a => a.AppointmentId == dto.AppointmentId && !a.Cancel)
            .FirstOrDefaultAsync();
        if (appointment == null)
            return BadRequest("Invalid or cancelled AppointmentId");

        
        if (dto.DoctorId.HasValue && !await _context.Users.AnyAsync(u => u.UserId == dto.DoctorId))
            return BadRequest("Invalid DoctorId");

      
        if (dto.DonationDate.Date < DateTime.Today)
            return BadRequest("DonationDate cannot be in the past");

       
        if (!new[] { "A", "B", "AB", "O" }.Contains(dto.BloodGroup))
            return BadRequest("Invalid BloodGroup. Must be A, B, AB, or O");
        if (!new[] { "Rh+", "Rh-" }.Contains(dto.RhType))
            return BadRequest("Invalid RhType. Must be Rh+ or Rh-");

        donation.AppointmentId = dto.AppointmentId;
        donation.DonationDate = dto.DonationDate;
        donation.BloodGroup = dto.BloodGroup;
        donation.RhType = dto.RhType;
        donation.DoctorId = dto.DoctorId;
        donation.Notes = dto.Notes;
        donation.IsSuccess = dto.IsSuccess;

        await _context.SaveChangesAsync();

        var responseDto = new BloodDonationHistoryDTO
        {
            DonationId = donation.DonationId,
            AppointmentId = donation.AppointmentId,
            UserId = appointment.UserId,
            DonationDate = donation.DonationDate,
            BloodGroup = donation.BloodGroup,
            RhType = donation.RhType,
            DoctorId = donation.DoctorId,
            Notes = donation.Notes,
            CreatedAt = donation.CreatedAt,
            IsSuccess = donation.IsSuccess
        };

        return Ok(responseDto);
    }

 
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBloodDonationHistory(int id)
    {
        var donation = await _context.BloodDonationHistories.FindAsync(id);
        if (donation == null)
            return NotFound();

        _context.BloodDonationHistories.Remove(donation);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Blood donation history deleted" });
    }
   
    [HttpPost("{id}/blood-info")]
    public async Task<IActionResult> PostBloodGroupAndRhType(int id, [FromBody] BloodGroupUpdateDTO dto)
    {
        var donation = await _context.BloodDonationHistories.FindAsync(id);
        if (donation == null)
            return NotFound("Donation record not found.");

        if (string.IsNullOrWhiteSpace(dto.BloodGroup) || !new[] { "A", "B", "AB", "O" }.Contains(dto.BloodGroup))
            return BadRequest("Invalid BloodGroup. Must be A, B, AB, or O.");

        if (string.IsNullOrWhiteSpace(dto.RhType) || !new[] { "Rh+", "Rh-" }.Contains(dto.RhType))
            return BadRequest("Invalid RhType. Must be Rh+ or Rh-.");

        donation.BloodGroup = dto.BloodGroup;
        donation.RhType = dto.RhType;

        await _context.SaveChangesAsync();

        return Ok(new
        {
            message = "Blood group and Rh type updated successfully",
            donationId = donation.DonationId,
            bloodGroup = donation.BloodGroup,
            rhType = donation.RhType
        });
    }

}