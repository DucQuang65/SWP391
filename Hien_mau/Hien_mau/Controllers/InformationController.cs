using Azure.Core;
using FirebaseAdmin.Auth.Hash;
using Hien_mau.Data;
using Hien_mau.Dto;
using Hien_mau.Interface;
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
        private readonly IInformationService _service;

        public InformationController(IInformationService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _service.GetUsers();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _service.GetUserById(id);

            if (user == null) 
                return NotFound();

            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(UserDto dto)
        {
            var createdUser = await _service.AddUser(dto);
            if (createdUser == null) 
                return BadRequest("Email đã tồn tại hoặc dữ liệu không hợp lệ.");

            return CreatedAtAction(nameof(GetUserById), new { id = createdUser.UserID }, createdUser);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UserDto dto)
        {
            var updated = await _service.UpdateUser(id, dto);

            if (!updated) 
                return NotFound("Không thể cập nhật.");

            return Ok();
        }

        [HttpPatch("{id}/distance")]
        public async Task<IActionResult> PatchDistance(int id, UpdateDistanceDto dto)
        {
            var updated = await _service.PatchDistance(id, dto.Distance);

            if (!updated) 
                return NotFound();
            return Ok();

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var result = await _service.DeleteUser(id);

            if (!result) 
                return NotFound();
            return Ok();

        }

        [HttpPut("{id}/self-reported-donation")]
        public async Task<IActionResult> UpdateSelfReportedDonationDate(int id, [FromBody] UpdateLastDonationDto dto)
        {
            var result = await _service.UpdateSelfReportedDonationDate(id, dto.SelfReportedLastDonationDate);

            if (!result)
                return NotFound("Không tìm thấy người dùng");

            return Ok(new { Message = "Cập nhật thành công ngày hiến máu tự khai." });
        }

    }
}
