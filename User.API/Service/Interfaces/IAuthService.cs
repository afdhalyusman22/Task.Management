using Library.Backend.Helpers;
using User.API.Models.Auth;

namespace User.API.Service.Interfaces
{
    public interface IAuthService
    {
        Task<(ValidationError? error, TokenDTO? token)> AuthenticateAsync(LoginDTO dto, CancellationToken cancellationToken);
        Task<ValidationError> RegisterAsync(RegisterDTO dto, CancellationToken cancellationToken);
    }
}
