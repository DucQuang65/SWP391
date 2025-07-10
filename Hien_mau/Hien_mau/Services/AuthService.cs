using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Hien_mau.Data;
using Hien_mau.Dto;
using Hien_mau.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace Hien_mau.Services
{
    public class AuthService : IAuthService
    {
        private readonly Hien_mauContext _context;
        private readonly IConfiguration _configuration;

        public AuthService(Hien_mauContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<string> LoginAsync(UserDto request)
        {           
            var user = await _context.Users.FirstOrDefaultAsync
                (u => u.Email == request.Email && u.Password == request.Password);

            if (user is null)
            {
                return null;
            }

            if (user.Status == 0)
                return ("Account is banned");

            return CreateToken(user);
        }

        public async Task<Users?> RegisterAsync(UserDto request)
        {            
            if (await _context.Users.AnyAsync(u => u.Email == request.Email))
            {
                return null;
            }
            var user = new Users()
            {
                Email = request.Email,
                Password = request.Password,
                Status = 1,
                RoleId = 1,
                CreatedAt = DateTime.UtcNow
            };

            //user.Email = request.Email;
            //user.Password = request.Password;

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<Users?> GetUserByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<Users> CreateUserFromGoogleAsync(string email, string name)
        {
            var vietNamTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
            var vietNamTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, vietNamTimeZone);

            var user = new Users
            {
                Email = email,
                //Name = name,
                Status = 1,
                RoleId = 1,
                CreatedAt = vietNamTime
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public string CreateToken(Users user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Role, user.RoleId.ToString())               
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration.GetValue<string>("AppSettings:Token")!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var tokenDescriptor = new JwtSecurityToken(
                issuer: _configuration.GetValue<string>("AppSettings:Issuer"),
                audience: _configuration.GetValue<string>("AppSettings:Audience"),
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }
    }
}
