﻿using Azure.Core;
using FirebaseAdmin.Auth.Hash;
using Hien_mau.Data;
using Hien_mau.Dto;
using Hien_mau.Models;
using Hien_mau.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Hien_mau.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InformationController : ControllerBase
    {
        private readonly Hien_mauContext _context;

        public InformationController(Hien_mauContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<InformationDto>>> GetUsers()
        {
            var users = await _context.Users
                .Select(u => new InformationDto
        {
                UserID = u.UserId,
                Email = u.Email,
                Phone = u.Phone,
                IDCardType = u.IdcardType,
                IDCard = u.Idcard,
                Name = u.Name,
                DateOfBirth = u.DateOfBirth,
                Age = u.Age,
                Gender = u.Gender,
                City = u.City,
                District = u.District,
                Ward = u.Ward,
                Address = u.Address,
                Distance = u.Distance,
                BloodGroup = u.BloodGroup,
                RhType = u.RhType,
                Weight = u.Weight,
                Height = u.Height,
                Status = u.Status,
                RoleID = u.RoleId,
                DepartmentId = u.DepartmentId,
                CreatedAt = u.CreatedAt
            }).ToListAsync();

            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Users>> GetUserById(int id)
        {
            var user = await _context.Users
            .Where(u => u.UserId == id)
            .Select(u => new InformationDto{
                UserID = u.UserId,  
                Email = u.Email,
                Phone = u.Phone,
                IDCardType = u.IdcardType,
                IDCard = u.Idcard,
                Name = u.Name,
                DateOfBirth = u.DateOfBirth,
                Age = u.Age,
                Gender = u.Gender,
                City = u.City,
                District = u.District,
                Ward = u.Ward,
                Address = u.Address,
                Distance = u.Distance,
                BloodGroup = u.BloodGroup,
                RhType = u.RhType,
                Weight = u.Weight,
                Height = u.Height,
                Status = u.Status,
                RoleID = u.RoleId,
                DepartmentId = u.DepartmentId,
                CreatedAt = u.CreatedAt
            }).FirstOrDefaultAsync();

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<Users>> AddUser(InformationDto informationDto)
        {            
            if (informationDto == null || string.IsNullOrEmpty(informationDto.Email))
                return BadRequest();

            var emailExists = await _context.Users.AnyAsync(u => u.Email == informationDto.Email);
            if (emailExists)
                return BadRequest();

            var vietNamTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
            var vietNamTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, vietNamTimeZone);

            var newUser = new Users
            {
                Email = informationDto.Email,
                Password = informationDto.Password,
                Phone = informationDto.Phone,
                IdcardType = informationDto.IDCardType,
                Idcard = informationDto.IDCard,
                Name = informationDto.Name,
                DateOfBirth = informationDto.DateOfBirth,
                Age = informationDto.Age,
                Gender = informationDto.Gender,
                City = informationDto.City,
                District = informationDto.District,
                Ward = informationDto.Ward,
                Address = informationDto.Address,
                Distance = informationDto.Distance,
                BloodGroup = informationDto.BloodGroup,
                RhType = informationDto.RhType,
                Status = informationDto.Status,
                RoleId = informationDto.RoleID,
                DepartmentId = informationDto.DepartmentId,
                CreatedAt = vietNamTime
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            informationDto.UserID = newUser.UserId;
            informationDto.CreatedAt = newUser.CreatedAt;

            return CreatedAtAction(nameof(GetUserById), new { id = newUser.UserId }, informationDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, InformationDto informationDto, [FromServices] NotificationLog logger)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return NotFound();

            if (!string.IsNullOrEmpty(informationDto.Email) && informationDto.Email != user.Email)
            {
                var emailExists = await _context.Users.AnyAsync(u => u.Email == informationDto.Email && u.UserId != id);
                if (emailExists)
                    return BadRequest();
                user.Email = informationDto.Email;
            }
            if (!string.IsNullOrEmpty(informationDto.Password))
                user.Password = informationDto.Password;

            user.Phone = informationDto.Phone;
            user.IdcardType = informationDto.IDCardType;
            user.Idcard = informationDto.IDCard;
            user.Name = informationDto.Name;
            user.DateOfBirth = informationDto.DateOfBirth;
            user.Age = informationDto.Age;
            user.Gender = informationDto.Gender;
            user.City = informationDto.City;
            user.District = informationDto.District;
            user.Ward = informationDto.Ward;
            user.Address = informationDto.Address;
            //user.Distance = informationDto.Distance;
            user.BloodGroup = informationDto.BloodGroup;
            user.RhType = informationDto.RhType;
            user.Weight = informationDto.Weight;
            user.Height = informationDto.Height;
            user.Status = informationDto.Status;
            user.RoleId = informationDto.RoleID;
            user.DepartmentId = informationDto.DepartmentId;

            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            // Ghi log sau khi đã lưu với UserId tương ứng
            await logger.NotiLog(id, "Profile", $"Sửa hồ sơ:", "Update");
            return Ok();
        }

        [HttpPatch("{id}/distance")]
        public async Task<IActionResult> PatchDistance(int id, UpdateDistanceDto updateDistanceDto)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            user.Distance = updateDistanceDto.Distance;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var User = await _context.Users.FindAsync(id);
            if (User == null)
                return NotFound();

            User.Status = 0; 

            _context.Users.Update(User);
            await _context.SaveChangesAsync();

            return Ok();
        }
        [HttpPut("{id}/self-reported-donation")]
        public async Task<IActionResult> UpdateSelfReportedDonationDate(int id, [FromBody] UpdateLastDonationDto dto)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return NotFound("Không tìm thấy người dùng");

            user.SelfReportedLastDonationDate = dto.SelfReportedLastDonationDate;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Cập nhật thành công ngày hiến máu tự khai." });
        }

    }
}
