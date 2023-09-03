using System.ComponentModel.DataAnnotations;

namespace User.API.Models.User
{
    public class UpdateDto
    {
        [Required(ErrorMessage = "FullName harus diisi")]
        public string FullName { get; set; } = null!;

        public string? PhoneNumber { get; set; }

        [Required(ErrorMessage = "Gender harus diisi")]
        public int GenderId { get; set; }
    }
}
