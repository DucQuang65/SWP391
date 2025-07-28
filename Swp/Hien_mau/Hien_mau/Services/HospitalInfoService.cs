using Hien_mau.Data;
using Hien_mau.Dto;
using Hien_mau.Interface;
using Microsoft.EntityFrameworkCore;

namespace Hien_mau.Services
{
    public class HospitalInfoService : IHospitalInfo
    {
        private readonly Hien_mauContext _context;
        public HospitalInfoService(Hien_mauContext context)
        {
            _context = context;
        }
        public async Task<List<HospitalInfoDto>> GetHospitalInfo()
        {
            return await _context.HospitalInfos.Select(h => new HospitalInfoDto
            {
                Id = h.Id,
                Name = h.Name,
                Address = h.Address,
                Phone = h.Phone,
                Email = h.Email,
                WorkingHours = h.WorkingHours,
                MapImageUrl = h.MapImageUrl,
                Latitude = h.Latitude,
                Longitude = h.Longitude
            }).ToListAsync();
        }
    }
}
