using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Hien_mau.Data;
using Hien_mau.Dto;
using Hien_mau.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Net.Mail;
using System.Net;
using Hien_mau.Interface;

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

        public async Task<string> LoginAsync(LoginDto request)
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

        public async Task<Users?> RegisterAsync(LoginDto request)
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
            email = email.Trim().ToLower();

            return await _context.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == email);
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
                Encoding.UTF8.GetBytes(_configuration.GetValue<string>("Jwt:Token")!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var tokenDescriptor = new JwtSecurityToken(
                issuer: _configuration.GetValue<string>("Jwt:Issuer"),
                audience: _configuration.GetValue<string>("Jwt:Audience"),
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }

        public string GeneratePasswordResetToken(string email)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Token"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                          new Claim(ClaimTypes.Email, email),
                          new Claim("ResetPassword", "true") // Đánh dấu token này là cho reset password
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<string> GeneratePasswordResetTokenAsync(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email && u.Status != 0);
            if (user == null) return null;

            var token = GeneratePasswordResetToken(email);
            var resetLink = $"http://localhost:5173/reset-password?token={token}";

            await SendResetPasswordEmailAsync(email, resetLink);
            return token;
        }

        public async Task<bool> ResetPasswordAsync(string token, string newPassword)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Token"]);

            try
            {
                var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var email = principal.FindFirst(ClaimTypes.Email)?.Value;
                var isReset = principal.FindFirst("ResetPassword")?.Value == "true";

                if (!isReset || string.IsNullOrEmpty(email)) 
                    return false;

                var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
                if (user == null) 
                    return false;

                user.Password = newPassword; 

                _context.Users.Update(user);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }


        public async Task SendResetPasswordEmailAsync(string email, string resetLink)
        {
            var smtpServer = _configuration["EmailSettings:SmtpServer"];
            var smtpPort = int.Parse(_configuration["EmailSettings:SmtpPort"]);
            var senderEmail = _configuration["EmailSettings:SenderEmail"];
            var senderPassword = _configuration["EmailSettings:SenderPassword"];

            var mail = new MailMessage
            {
                From = new MailAddress(senderEmail),
                Subject = "Reset Your Password",
                Body = $"Nhấn vào liên kết sau để đặt lại mật khẩu: " +
                       $"<a href=\"{resetLink}\">Đặt lại mật khẩu</a><br><br>" +
                       $"Liên kết này sẽ hết hạn sau 30 phút.<br><br>" +
                       $"Nếu bạn không yêu cầu đặt lại mật khẩu, vui lòng bỏ qua email này.",
                IsBodyHtml = true
            };

            mail.To.Add(email);

            using var smtp = new SmtpClient(smtpServer)
            {
                Port = smtpPort,
                Credentials = new NetworkCredential(senderEmail, senderPassword),
                EnableSsl = true
            };

            await smtp.SendMailAsync(mail);
        }

        public async Task<bool> ChangePasswordAsync(int userId, ChangePasswordDto dto)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null || user.Password != dto.CurrentPassword)
                return false;

            user.Password = dto.NewPassword;

            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
