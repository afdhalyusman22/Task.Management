using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using User.API.Models.User;
using User.API.Services;
using User.API.Services.Interfaces;

namespace User.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IdentityService _identityService;

        public UserController(IUserService userService, IdentityService identityService)
        {
            _userService = userService;
            _identityService = identityService;
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateDTO dto, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            if (_identityService.UserId == null)
            {
                return Unauthorized();
            }
            var userId = new Guid(_identityService.UserId);

            var error = await _userService.UpdateAsync(userId, dto, cancellationToken);
            if (error != null)
            {
                ModelState.AddModelError(error.Field, error.Message);
                return ValidationProblem(ModelState);
            }
            return Ok("Success Update User");
        }

        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            if (_identityService.UserId == null)
            {
                return Unauthorized();
            }
            var userId = new Guid(_identityService.UserId);

            var (error, user) = await _userService.GetAsync(userId, cancellationToken);
            if (error != null)
            {
                ModelState.AddModelError(error.Field, error.Message);
                return ValidationProblem(ModelState);
            }
            return Ok(user);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(CancellationToken cancellationToken)
        {
            if (_identityService.UserId == null)
            {
                return Unauthorized();
            }
            var userId = new Guid(_identityService.UserId);

            var error = await _userService.DeleteAsync(userId, cancellationToken);
            if (error != null)
            {
                ModelState.AddModelError(error.Field, error.Message);
                return ValidationProblem(ModelState);
            }
            return Ok();
        }
    }
}
