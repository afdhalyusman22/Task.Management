namespace User.API.Models.User
{
    public class DetailDto
    {
        public Guid UserId { get; set; }

        public string Email { get; set; } = null!;

        public string FullName { get; set; } = null!;

        public string? PhoneNumber { get; set; }

        public int GenderId { get; set; }
        public string GenderName { get; set; }

        public DateTime LastLogin { get; set; }

        public DateTime RegisterAt { get; set; }
    }
}
