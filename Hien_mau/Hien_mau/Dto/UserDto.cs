using System.ComponentModel.DataAnnotations;

namespace Hien_mau.Dto
{
    public class UserDto
    {
        [Required, EmailAddress]
        public string Email { get; set; } = null!;
        [Required]
        [MinLength(6)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z\d]).{6,}$",
        ErrorMessage = "Mật khẩu phải có ít nhất 6 ký tự, gồm chữ thường, chữ hoa, số và ký tự đặc biệt.")]
        public string? Password { get; set; } // Used for client-side registration
        //public string IdToken { get; set; } = null!; // Sent by client after Firebase auth
    }
}
