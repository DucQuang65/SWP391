using Hien_mau.Data;
using Hien_mau.Dto;
using Hien_mau.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public async Task<ActionResult<List<User>>> GetUsers()
        {
            var users = await _context.Users.Select(u => new InformationDto
        {
                UserID = u.UserId,
                Email = u.Email,
                Password = u.Password,
                Phone = u.Phone,
                IDCardType = u.IdcardType,
                IDCard = u.Idcard,
                Name = u.Name,
                DateOfBirth = (DateTime)u.DateOfBirth,
                Age = (int)u.Age,
                Gender = u.Gender,
                City = u.City,
                District = u.District,
                Ward = u.Ward,
                Address = u.Address,
                BloodGroup = u.BloodGroup,
                RhType = u.RhType,
                Status = u.Status,
                RoleID = u.RoleId,
                Department = u.Department,
                CreatedAt = (DateTime)u.CreatedAt
            }).ToListAsync();

            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUserByID(int id)
        {
            var user = await _context.Users.FindAsync(id); 
            if (user == null)
                return NotFound();

            var information = new InformationDto
            {
                UserID = user.UserId,
                Email = user.Email,
                Password = user.Password,
                Phone = user.Phone,
                IDCardType = user.IdcardType,
                IDCard = user.Idcard,
                Name = user.Name,
                DateOfBirth = (DateTime)user.DateOfBirth,
                Age = (int)user.Age,
                Gender = user.Gender,
                City = user.City,
                District = user.District,
                Ward = user.Ward,
                Address = user.Address,
                BloodGroup = user.BloodGroup,
                RhType = user.RhType,
                Status = user.Status,
                RoleID = user.RoleId,
                Department = user.Department,
                CreatedAt = (DateTime)user.CreatedAt
            };

            return Ok(information);
        }

        [HttpPost]
        public async Task<ActionResult<User>> AddUser(InformationDto informationDto)
        {
            //if (newUser == null)
            //    return BadRequest();

            //_context.Users.Add(newUser);
            //await _context.SaveChangesAsync();
            //return CreatedAtAction(nameof(GetUserByID), new { id = newUser.UserId }, newUser);

            if (informationDto == null)
                return BadRequest();

            var newUser = new User
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
                BloodGroup = informationDto.BloodGroup,
                RhType = informationDto.RhType,
                Status = informationDto.Status,
                RoleId = informationDto.RoleID,
                Department = informationDto.Department,
                CreatedAt = DateTime.UtcNow
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            // Map sang InformationDto để trả về
            var result = new InformationDto
            {
                Email = informationDto.Email,
                Password = informationDto.Password,
                Phone = informationDto.Phone,
                IDCardType = informationDto.IDCardType,
                IDCard = informationDto.IDCard,
                Name = informationDto.Name,
                DateOfBirth = informationDto.DateOfBirth,
                Age = informationDto.Age,
                Gender = informationDto.Gender,
                City = informationDto.City,
                District = informationDto.District,
                Ward = informationDto.Ward,
                Address = informationDto.Address,
                BloodGroup = informationDto.BloodGroup,
                RhType = informationDto.RhType,
                Status = informationDto.Status,
                RoleID = informationDto.RoleID,
                Department = informationDto.Department,
                CreatedAt = informationDto.CreatedAt
            };

            return CreatedAtAction(nameof(GetUserByID), new { id = informationDto.UserID }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, InformationDto informationDto)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return NotFound();

            user.Email = informationDto.Email;
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
            user.BloodGroup = informationDto.BloodGroup;
            user.RhType = informationDto.RhType;
            user.Status = informationDto.Status;
            user.RoleId = informationDto.RoleID;
            user.Department = informationDto.Department;
            user.CreatedAt = DateTime.UtcNow;

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var User = await _context.Users.FindAsync(id);
            if (User == null)
                return NotFound();

            _context.Users.Remove(User);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
