using Hien_mau.Dto;
using Hien_mau.Models;

namespace Hien_mau.Services
{
    public interface IAuthService
    {
        Task<User?> RegisterAsync(UserDto request);
        Task<string?> LoginAsync(UserDto request);
        Task<User?> GetUserByEmailAsync(string email);
        Task<User> CreateUserFromGoogleAsync(string email, string name);
        string CreateToken(User user);
    }
}
