using Hien_mau.Dto;

namespace Hien_mau.Interface
{
    public interface IHospitalInfo
    {
        Task<List<HospitalInfoDto>> GetHospitalInfo();
    }
}
