using Hien_mau.Dto;

namespace Hien_mau.Interface
{
    public interface IBloodRequestService
    {
        Task<List<BloodRequestDto>> GetAllBloodRequest(HttpRequest request);
        Task<BloodRequestDto?> GetBloodRequestById(int id, HttpRequest request);
        Task<BloodRequestDto?> CreateBloodRequest(BloodRequestDto dto, HttpRequest request);
        Task<bool> UpdateBloodRequest(int id, BloodRequestDto dto);
        Task<bool> PatchStatusBloodRequest(int id, byte status,string? note = null);
        Task<bool> DeleteBloodRequest(int id);
    }
}
