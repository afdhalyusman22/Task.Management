using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Task.API.Service.Interfaces;

namespace Task.API.Controllers
{
    [Authorize]
    [Route("api/task-status")]
    [ApiController]
    public class TaskStatusController : Controller
    {
        private readonly ITaskStatusService _taskStatusService;

        public TaskStatusController(ITaskStatusService taskStatusService)
        {
            _taskStatusService = taskStatusService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            var res = await _taskStatusService.GetListAsync(cancellationToken);
            return Ok(res);
        }
    }
}
