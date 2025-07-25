using Hien_mau.Data;
using Hien_mau.Dto;
using Hien_mau.Interface;
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
        private readonly IBloodRequestService _service;

        public BloodRequestController(IBloodRequestService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetBloodRequest()
        {
            var result = await _service.GetAllBloodRequest(Request);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRequestById(int id)
        {
            var result = await _service.GetBloodRequestById(id, Request);

            if (result == null) 
                return NotFound();
            return Ok(result);

        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> CreateBloodRequest([FromForm] BloodRequestDto dto)
        {
            var result = await _service.CreateBloodRequest(dto, Request);

            if (result == null) 
                return BadRequest("Yêu cầu không hợp lệ hoặc đã tồn tại.");

            return CreatedAtAction(nameof(GetRequestById), new { id = result.RequestId }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBloodRequest(int id, BloodRequestDto dto)
        {
            var success = await _service.UpdateBloodRequest(id, dto);

            if (!success) 
                return BadRequest();

            return Ok();
        }

        [HttpPatch("{id}/status")]
        public async Task<IActionResult> PatchStatus(int id, UpdateStatusBloodRequestDto dto)
        {
            var success = await _service.PatchStatusBloodRequest(id, dto.Status,dto.Note);
            return success ? Ok() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBloodRequest(int id)
        {
            var success = await _service.DeleteBloodRequest(id);
            return success ? Ok() : NotFound();
        }

    }
}
