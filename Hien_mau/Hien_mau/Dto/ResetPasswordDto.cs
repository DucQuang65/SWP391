using System.ComponentModel.DataAnnotations;

namespace Hien_mau.Dto
{
    public class ResetPasswordDto
    {
        public string Token { get; set; }

        [Required]
        [MinLength(6)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z\d]).{6,}$",
        ErrorMessage = "Mật khẩu phải có ít nhất 6 ký tự, gồm chữ thường, chữ hoa, số và ký tự đặc biệt.")]
        public string NewPassword { get; set; }
    }
}
