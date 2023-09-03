using Library.Backend.Helpers;
using User.API.Models.Gender;

namespace User.API.Service.Interfaces
{
    public interface IGenderServices
    {
        Task<List<GenderDto>> GetListAsync(CancellationToken cancellationToken);
    }
}
