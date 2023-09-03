using Library.Backend.Helpers;
using User.API.Models.User;

namespace User.API.Services.Interfaces
{
    public interface IUserService
    {
        Task<ValidationError> UpdateAsync(Guid userId, UpdateDTO dto, CancellationToken cancellationToken);
        Task<(ValidationError error, DetailDTO user)> GetAsync(Guid userId, CancellationToken cancellationToken);
        Task<ValidationError> DeleteAsync(Guid userId, CancellationToken cancellationToken);
    }
}
