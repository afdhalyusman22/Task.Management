using Task.API.Models.TaskStatus;

namespace Task.API.Service.Interfaces
{
    public interface ITaskStatusService
    {
        Task<List<TaskStatusDto>> GetListAsync(CancellationToken cancellationToken);
    }
}
