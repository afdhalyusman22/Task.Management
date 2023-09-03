using Library.Backend.Helpers;
using Task.API.Models.Task;

namespace Task.API.Service.Interfaces
{
    public interface ITaskService
    {
        Task<List<TaskDto>> GetListAsync(CancellationToken cancellationToken);
        Task<List<TaskDto>> GetListAsync(Guid userId, CancellationToken cancellationToken);
        Task<(ValidationError? error, TaskDto? task)> GetAsync(Guid userId, Guid taskId, CancellationToken cancellationToken);
        Task<ValidationError> CreateAsync(Guid userId, CreateUpdateTaskDto dto, CancellationToken cancellationToken);
        Task<ValidationError> UpdateAsync(Guid userId, Guid taskId, CreateUpdateTaskDto dto, CancellationToken cancellationToken);
        Task<ValidationError> DeleteAsync(Guid userId, Guid taskId, CancellationToken cancellationToken);
    }
}
