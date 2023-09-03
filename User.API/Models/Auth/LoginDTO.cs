using System.ComponentModel.DataAnnotations;

namespace User.API.Models.Auth
{
    public class LoginDto
    {
        [Required]
        public string PhoneEmail { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
