using Microsoft.AspNetCore.Mvc;
using User.API.Models.Auth;
using User.API.Service.Interfaces;

namespace User.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IAuthService _authServices;

        public AuthController(IAuthService authServices)
        {
            _authServices = authServices;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Authenticate([FromBody] LoginDto dto, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            var (error, token) = await _authServices.AuthenticateAsync(dto, cancellationToken);
            if (error != null)
            {
                ModelState.AddModelError(error.Field, error.Message);
                return ValidationProblem(ModelState);
            }
            return Ok(token);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            var error = await _authServices.RegisterAsync(dto, cancellationToken);
            if (error != null)
            {
                ModelState.AddModelError(error.Field, error.Message);
                return ValidationProblem(ModelState);
            }
            return Ok();
        }
    }
}
