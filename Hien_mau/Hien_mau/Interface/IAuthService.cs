using Hien_mau.Dto;
using Hien_mau.Models;

namespace Hien_mau.Interface
{
    public interface IAuthService
    {
        Task<Users?> RegisterAsync(LoginDto request);
        Task<string?> LoginAsync(LoginDto request);
        Task<Users?> GetUserByEmailAsync(string email);
        Task<Users> CreateUserFromGoogleAsync(string email, string name);
        string CreateToken(Users user);
        Task<string> GeneratePasswordResetTokenAsync(string email);
        Task<bool> ResetPasswordAsync(string token, string newPassword);
        Task SendResetPasswordEmailAsync(string email, string resetLink);
        Task<bool> ChangePasswordAsync(int userId, ChangePasswordDto dto);
    }
}
