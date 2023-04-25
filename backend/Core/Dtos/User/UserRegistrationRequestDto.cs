using System.ComponentModel.DataAnnotations;

namespace backend.Core.Dtos.User
{
    public class UserRegistrationRequestDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Email { get; set; }
    }
}
