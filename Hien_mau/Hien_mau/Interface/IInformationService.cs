using Hien_mau.Dto;

namespace Hien_mau.Interface
{
    public interface IInformationService
    {
        Task<List<UserDto>> GetUsers();
        Task<UserDto?> GetUserById(int id);
        Task<UserDto?> AddUser(UserDto dto);
        Task<bool> UpdateUser(int id, UserDto dto);
        Task<bool> PatchDistance(int id, double distance);
        Task<bool> DeleteUser(int id);
        Task<bool> UpdateSelfReportedDonationDate(int id, DateTime? date);
    }
}
