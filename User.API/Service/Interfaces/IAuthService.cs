using Library.Backend.Helpers;
using User.API.Models.Auth;

namespace User.API.Service.Interfaces
{
    public interface IAuthService
    {
        Task<(ValidationError? error, TokenDto? token)> AuthenticateAsync(LoginDto dto, CancellationToken cancellationToken);
        Task<ValidationError> RegisterAsync(RegisterDto dto, CancellationToken cancellationToken);
    }
}
