using Hien_mau.Data;
using Hien_mau.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hien_mau.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : Controller
    {
        private readonly Hien_mauContext _context;

        public PatientController(Hien_mauContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<PatientDto>>> GetPatients()
        {
            var patients = await _context.Patients
                .Select(u => new PatientDto
                {
                    PatientId = u.PatientId,
                    FullName = u.FullName,
                    Gender = u.Gender,
                    DateOfBirth = u.DateOfBirth,
                    Age = u.Age,
                    Phone = u.Phone,
                    Address = u.Address,
                    Email = u.Email
                }).ToListAsync();

            return Ok(patients);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PatientDto>> GetPatientById(int id)
        {
            var patient = await _context.Patients
                .Where(p => p.PatientId == id)
                .Select(p => new PatientDto
                {
                    PatientId = p.PatientId,
                    FullName = p.FullName,
                    Gender = p.Gender,
                    DateOfBirth = p.DateOfBirth,
                    Age = p.Age,
                    Phone = p.Phone,
                    Address = p.Address,
                    Email = p.Email
                })
                .FirstOrDefaultAsync();

            if (patient == null)
            {
                return NotFound();
            }

            return Ok(patient);
        }
    }
}
