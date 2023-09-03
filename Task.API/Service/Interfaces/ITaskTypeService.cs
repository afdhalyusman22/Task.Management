using Task.API.Models.TaskType;

namespace Task.API.Service.Interfaces
{
    public interface ITaskTypeService
    {
        Task<List<TaskTypeDto>> GetListAsync(CancellationToken cancellationToken);
    }
}
