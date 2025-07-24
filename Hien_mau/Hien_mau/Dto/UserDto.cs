using System.ComponentModel.DataAnnotations;

namespace Hien_mau.Dto
{
    public class UserDto
    {
        public int UserID { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string IDCardType { get; set; }
        public string IDCard { get; set; }
        public string Name { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int? Age { get; set; }
        public string Gender { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Ward { get; set; }
        public string Address { get; set; }
        public double? Distance { get; set; }
        public string BloodGroup { get; set; }
        public string RhType { get; set; }
        public double? Weight { get; set; }
        public double? Height { get; set; }
        public byte? Status { get; set; }
        public int RoleID { get; set; }
        public int? DepartmentId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? SelfReportedLastDonationDate { get; set; }
    }
    public class UpdateLastDonationDto
    {
        public DateTime SelfReportedLastDonationDate { get; set; }
    }
    public class UpdateDistanceDto
    {
        public double Distance { get; set; }
    }
    public class LoginDto
    {
        [Required, EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        [MinLength(6)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z\d]).{6,}$",
        ErrorMessage = "Mật khẩu phải có ít nhất 6 ký tự, gồm chữ thường, chữ hoa, số và ký tự đặc biệt.")]
        public string? Password { get; set; } 
    }
    public class ResetPasswordDto
    {
        public string Token { get; set; }

        [Required]
        [MinLength(6)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z\d]).{6,}$",
        ErrorMessage = "Mật khẩu phải có ít nhất 6 ký tự, gồm chữ thường, chữ hoa, số và ký tự đặc biệt.")]
        public string NewPassword { get; set; }
    }
    public class ChangePasswordDto
    {
        [Required]
        public string CurrentPassword { get; set; }

        [Required]
        [MinLength(6)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z\d]).{6,}$",
        ErrorMessage = "Mật khẩu mới phải có ít nhất 6 ký tự, gồm chữ thường, chữ hoa, số và ký tự đặc biệt.")]
        public string NewPassword { get; set; }
    }
}
