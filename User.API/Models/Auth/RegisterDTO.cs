using System.ComponentModel.DataAnnotations;

namespace User.API.Models.Auth
{
    public class RegisterDto
    {
        [Required]
        public string Email { get; set; } = null!;

        [Required]
        public string FullName { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;

        public string? PhoneNumber { get; set; }

        [Required]
        public int GenderId { get; set; }
    }
}
