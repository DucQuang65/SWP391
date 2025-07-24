using Hien_mau.Data;
using Hien_mau.Dto;
using Hien_mau.Interface;
using Hien_mau.Models;
using Microsoft.EntityFrameworkCore;

namespace Hien_mau.Services
{
    public class InformationService : IInformationService
    {
        private readonly Hien_mauContext _context;
        private readonly NotificationLog _logger;

        public InformationService(Hien_mauContext context, NotificationLog logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<UserDto>> GetUsers()
        {
            return await _context.Users.Select(u => new UserDto
            {
                UserID = u.UserId,
                Email = u.Email,
                Phone = u.Phone,
                IDCardType = u.IdcardType,
                IDCard = u.Idcard,
                Name = u.Name,
                DateOfBirth = u.DateOfBirth,
                Age = u.Age,
                Gender = u.Gender,
                City = u.City,
                District = u.District,
                Ward = u.Ward,
                Address = u.Address,
                Distance = u.Distance,
                BloodGroup = u.BloodGroup,
                RhType = u.RhType,
                Weight = u.Weight,
                Height = u.Height,
                Status = u.Status,
                RoleID = u.RoleId,
                DepartmentId = u.DepartmentId,
                CreatedAt = u.CreatedAt,
                SelfReportedLastDonationDate = u.SelfReportedLastDonationDate
            }).ToListAsync();
        }

        public async Task<UserDto?> GetUserById(int id)
        {
            return await _context.Users.Where(u => u.UserId == id).Select(u => new UserDto
            {
                UserID = u.UserId,
                Email = u.Email,
                Phone = u.Phone,
                IDCardType = u.IdcardType,
                IDCard = u.Idcard,
                Name = u.Name,
                DateOfBirth = u.DateOfBirth,
                Age = u.Age,
                Gender = u.Gender,
                City = u.City,
                District = u.District,
                Ward = u.Ward,
                Address = u.Address,
                Distance = u.Distance,
                BloodGroup = u.BloodGroup,
                RhType = u.RhType,
                Weight = u.Weight,
                Height = u.Height,
                Status = u.Status,
                RoleID = u.RoleId,
                DepartmentId = u.DepartmentId,
                CreatedAt = u.CreatedAt,
                SelfReportedLastDonationDate = u.SelfReportedLastDonationDate
            }).FirstOrDefaultAsync();
        }

        public async Task<UserDto?> AddUser(UserDto dto)
        {
            if (dto == null || string.IsNullOrEmpty(dto.Email))
                return null;

            if (await _context.Users.AnyAsync(u => u.Email == dto.Email))
                return null;

            var timeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
            var vnTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZone);

            var newUser = new Users
            {
                Email = dto.Email,
                Password = dto.Password,
                Phone = dto.Phone,
                IdcardType = dto.IDCardType,
                Idcard = dto.IDCard,
                Name = dto.Name,
                DateOfBirth = dto.DateOfBirth,
                Age = dto.Age,
                Gender = dto.Gender,
                City = dto.City,
                District = dto.District,
                Ward = dto.Ward,
                Address = dto.Address,
                Distance = dto.Distance,
                BloodGroup = dto.BloodGroup,
                RhType = dto.RhType,
                Status = dto.Status,
                RoleId = dto.RoleID,
                DepartmentId = dto.DepartmentId,
                CreatedAt = vnTime,
                SelfReportedLastDonationDate = dto.SelfReportedLastDonationDate
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            dto.UserID = newUser.UserId;
            dto.CreatedAt = newUser.CreatedAt;

            return dto;
        }

        public async Task<bool> UpdateUser(int id, UserDto dto)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return false;

            if (!string.IsNullOrEmpty(dto.Email) && dto.Email != user.Email)
            {
                if (await _context.Users.AnyAsync(u => u.Email == dto.Email && u.UserId != id))
                    return false;
                user.Email = dto.Email;
            }

            if (!string.IsNullOrEmpty(dto.Password))
                user.Password = dto.Password;

            user.Phone = dto.Phone;
            user.IdcardType = dto.IDCardType;
            user.Idcard = dto.IDCard;
            user.Name = dto.Name;
            user.DateOfBirth = dto.DateOfBirth;
            user.Age = dto.Age;
            user.Gender = dto.Gender;
            user.City = dto.City;
            user.District = dto.District;
            user.Ward = dto.Ward;
            user.Address = dto.Address;
            user.BloodGroup = dto.BloodGroup;
            user.RhType = dto.RhType;
            user.Weight = dto.Weight;
            user.Height = dto.Height;
            user.Status = dto.Status;
            user.RoleId = dto.RoleID;
            user.DepartmentId = dto.DepartmentId;
            user.SelfReportedLastDonationDate = dto.SelfReportedLastDonationDate;

            _context.Users.Update(user);

            await _context.SaveChangesAsync();
            await _logger.NotiLog(id, "Profile", "Sửa hồ sơ:", "Update");

            return true;
        }

        public async Task<bool> PatchDistance(int id, double distance)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) 
                return false;

            user.Distance = distance;

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) 
                return false;

            user.Status = 0;

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateSelfReportedDonationDate(int id, DateTime? date)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) 
                return false;

            user.SelfReportedLastDonationDate = date;

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
