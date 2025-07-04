﻿using Hien_mau.Data;
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
        public BloodRequestController(Hien_mauContext context, NotificationLog logger)
        {
            _context = context;
            _logger = logger;
        }
        [HttpGet]
        public async Task<ActionResult<List<BloodRequestDto>>> GetBloodRequest()
        {
            var bloodRequests = await _context.BloodRequests
                .Select(x => new BloodRequestDto
                {
                    UserID = x.UserId,
                    RequestId = x.RequestId,
                    PatientID = x.PatientID,
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
                    Reason = x.Reason,
                    CreatedTime = x.CreatedTime
                }).ToListAsync();
            return Ok(bloodRequests);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<BloodRequest>> GetRequestById(int id)
        {
            var bloodRequests = await _context.BloodRequests
                .Select(x => new BloodRequestDto
                {
                    UserID = x.UserId,
                    RequestId = x.RequestId,
                    PatientID = x.PatientID,
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
                    Reason = x.Reason,
                    CreatedTime = x.CreatedTime
                }).FirstOrDefaultAsync();

            if (bloodRequests == null) 
                return NotFound();

            return Ok(bloodRequests);
        }
        [HttpPost]
        public async Task<IActionResult> CreateBloodRequest([FromBody] BloodRequestDto bloodRequestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }           

            var existingRequest = await _context.BloodRequests
            .Where(x => x.UserId == bloodRequestDto.UserID && (x.Status == 0 || x.Status == 1)) 
            .FirstOrDefaultAsync();

            if (existingRequest != null)
            {
                return BadRequest();
            }

            var vietNamTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
            var vietNamTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, vietNamTimeZone);

            var bloodRequest = new BloodRequest
            {
                UserId = bloodRequestDto.UserID,
                PatientID = bloodRequestDto.PatientID,
                PatientName = bloodRequestDto.PatientName,
                Age = bloodRequestDto.Age,
                Gender = bloodRequestDto.Gender,
                Relationship = bloodRequestDto.Relationship,
                BloodGroup = bloodRequestDto.BloodGroup,
                RhType = bloodRequestDto.RhType,
                Quantity = bloodRequestDto.Quantity,
                Reason = bloodRequestDto.Reason,
                Status = 0,
                CreatedTime = vietNamTime
            };

            if (bloodRequestDto.Relationship == "Doctor")
            {
                var doctorUser = await _context.Users.FindAsync(bloodRequestDto.UserID);
                if (doctorUser != null)
                {
                    bloodRequest.DoctorName = doctorUser.Name;
                    bloodRequest.DoctorPhone = doctorUser.Phone;
                    bloodRequest.IsAutoApproved = true;
                    bloodRequest.Status = 1;
                    bloodRequest.FacilityName = "Ánh Dương";
                }
            }
            else
            {
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

            bloodRequest.PatientID = bloodRequestDto.PatientID;
            bloodRequest.PatientName = bloodRequestDto.PatientName;
            bloodRequest.Age = bloodRequestDto.Age;
            bloodRequest.Gender = bloodRequestDto.Gender;
            bloodRequest.Relationship = bloodRequestDto.Relationship;
            bloodRequest.FacilityName = bloodRequestDto.FacilityName;
            bloodRequest.DoctorName = bloodRequestDto.DoctorName;
            bloodRequest.DoctorPhone = bloodRequestDto.DoctorPhone;
            bloodRequest.BloodGroup = bloodRequestDto.BloodGroup;
            bloodRequest.RhType = bloodRequestDto.RhType;
            bloodRequest.Quantity = bloodRequestDto.Quantity;
            bloodRequest.Reason = bloodRequestDto.Reason;


            _context.BloodRequests.Update(bloodRequest);
            await _context.SaveChangesAsync();
            // Ghi log sau khi đã lưu với UserId tương ứng
            await _logger.NotiLog(bloodRequest.UserId, "Yêu cầu máu", $"Sửa yêu cầu:", "Update");
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
