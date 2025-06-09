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
        public async Task<(string? Token, string? IdToken)> LoginAsync(UserDto request)
        {
            try
            {
                // Validate input
                if (string.IsNullOrEmpty(request.IdToken))
                {
                    throw new ArgumentException("IdToken is required");
                }

                //// Verify IdToken with FirebaseAdmin
                //var decodedToken = await FirebaseAuth.GetAuth(_firebaseApp).VerifyIdTokenAsync(request.IdToken);
                //var firebaseUid = decodedToken.Uid;

                // Find user in local database
                var localUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email && u.Password == request.Password);
                if (localUser == null)
                {
                    return (null, null); // User not registered in local DB
                }

                // Generate custom JWT
                var customToken = CreateToken(localUser);
                return (customToken, request.IdToken); // Return custom JWT and client-provided IdToken
            }
            catch (FirebaseAuthException ex)
            {
                Console.WriteLine($"Firebase login failed: {ex.Message}");
                return (null, null);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Invalid input: {ex.Message}");
                return (null, null);
            }
        }

        public async Task<User?> RegisterAsync(UserDto request)
        {
            try
            {
                // Validate input
                if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.IdToken))
                {
                    throw new ArgumentException("Email and IdToken are required");
                }

                // Check if email exists
                if (await _context.Users.AnyAsync(u => u.Email == request.Email))
                {
                    return null; // Email already exists
                }

                // Verify IdToken
                //var decodedToken = await FirebaseAuth.GetAuth(_firebaseApp).VerifyIdTokenAsync(request.IdToken);
                //var firebaseUid = decodedToken.Uid;

                // Create user in local database
                var localUser = new User
                {
                    Email = request.Email,
                    Password = null, // Firebase handles password
                    Status = 1,
                    RoleId = 1,
                    CreatedAt = DateTime.UtcNow
                };

                using var transaction = await _context.Database.BeginTransactionAsync();
                try
                {
                    _context.Users.Add(localUser);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch
                {
                    await transaction.RollbackAsync();
                    throw;
                }

                return localUser;
            }
            catch (FirebaseAuthException ex)
            {
                Console.WriteLine($"Firebase registration failed: {ex.Message}");
                return null;
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Invalid input: {ex.Message}");
                return null;
            }
        }

        private string CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim("FirebaseUid", user.FirebaseUid)
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
