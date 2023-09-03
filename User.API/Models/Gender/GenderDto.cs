using Library.Backend.Helpers;

namespace User.API.Models.Gender
{
    public class GenderDto : BaseDtoId<int>
    {
        public string Name { get; set; } = string.Empty;
    }
}
