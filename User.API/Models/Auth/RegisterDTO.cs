using System.ComponentModel.DataAnnotations;

namespace User.API.Models.Auth
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "Email harus diisi")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "FullName harus diisi")]
        public string FullName { get; set; } = null!;

        [Required(ErrorMessage = "Password harus diisi")]
        public string Password { get; set; } = null!;

        public string? PhoneNumber { get; set; }

        [Required(ErrorMessage = "Gender harus diisi")]
        public int GenderId { get; set; }
    }
}
