using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Task.API.Models.Task;
using Task.API.Service.Interfaces;
using Task.API.Services;

namespace Task.API.Controllers
{
    [Authorize]
    [Route("api/task")]
    [ApiController]
    public class TaskController : Controller
    {
        private readonly ITaskService _taskService;
        private readonly IdentityService _identityService;

        public TaskController(ITaskService taskService, IdentityService identityService)
        {
            _taskService = taskService;
            _identityService = identityService;
        }

        [HttpGet]
        public async Task<IActionResult> GetList(CancellationToken cancellationToken)
        {
            if (_identityService.UserId == null)
            {
                return Unauthorized();
            }
            var userId = new Guid(_identityService.UserId);

            var data = await _taskService.GetListAsync(userId, cancellationToken);

            return Ok(data);
        }

        [HttpGet("{taskId}")]
        public async Task<IActionResult> Get(Guid taskId, CancellationToken cancellationToken)
        {
            if (_identityService.UserId == null)
            {
                return Unauthorized();
            }
            var userId = new Guid(_identityService.UserId);

            var (error, data) = await _taskService.GetAsync(userId, taskId, cancellationToken);
            if (error != null)
            {
                ModelState.AddModelError(error.Field, error.Message);
                return ValidationProblem(ModelState);
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUpdateTaskDto dto, CancellationToken cancellationToken)
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

            var error = await _taskService.CreateAsync(userId, dto, cancellationToken);
            if (error != null)
            {
                ModelState.AddModelError(error.Field, error.Message);
                return ValidationProblem(ModelState);
            }
            return Ok("Success Create");
        }

        [HttpPut("{taskId}")]
        public async Task<IActionResult> Update([FromBody] CreateUpdateTaskDto dto, Guid taskId, CancellationToken cancellationToken)
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

            var error = await _taskService.UpdateAsync(userId, taskId, dto, cancellationToken);
            if (error != null)
            {
                ModelState.AddModelError(error.Field, error.Message);
                return ValidationProblem(ModelState);
            }
            return Ok("Success Update");
        }

        [HttpDelete("{taskId}")]
        public async Task<IActionResult> Delete(Guid taskId, CancellationToken cancellationToken)
        {
            if (_identityService.UserId == null)
            {
                return Unauthorized();
            }
            var userId = new Guid(_identityService.UserId);

            var error = await _taskService.DeleteAsync(userId, taskId, cancellationToken);
            if (error != null)
            {
                ModelState.AddModelError(error.Field, error.Message);
                return ValidationProblem(ModelState);
            }
            return Ok("Success Delete");
        }
    }
}
