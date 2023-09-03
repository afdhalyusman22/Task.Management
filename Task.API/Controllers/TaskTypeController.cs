using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Task.API.Service;
using Task.API.Service.Interfaces;

namespace Task.API.Controllers
{
    [Authorize]
    [Route("api/task-type")]
    [ApiController]
    public class TaskTypeController : Controller
    {
        private readonly ITaskTypeService _taskTypeService;

        public TaskTypeController(ITaskTypeService taskTypeService)
        {
            _taskTypeService = taskTypeService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            var res = await _taskTypeService.GetListAsync(cancellationToken);
            return Ok(res);
        }
    }
}
