using System.ComponentModel.DataAnnotations;

namespace Hien_mau.Dto
{
    public class UserDto
    {
        [Required, EmailAddress]
        public string Email { get; set; } = null!;

        public string? Password { get; set; } // Used for client-side registration
        //public string IdToken { get; set; } = null!; // Sent by client after Firebase auth
    }
}
