using Microsoft.EntityFrameworkCore;
using Task.API.Entities;
using Task.API.Models.Task;
using Task.API.Models.TaskStatus;
using Task.API.Service.Interfaces;

namespace Task.API.Service
{
    public class TaskService : ITaskService
    {
        private readonly DataContext _context;

        public TaskService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<TaskDto>> GetListAsync(CancellationToken cancellationToken)
        {
            var res = await _context.Tasks
                .Select(Q => new TaskDto
                {
                    CreatedAt = Q.CreatedAt,
                    EndDate = Q.EndDate,
                    PlannedEnd = Q.PlannedEnd,
                    PlannedStart = Q.PlannedStart,
                    StartDate = Q.StartDate,
                    TaskId = Q.TaskId,
                    TaskStatusId = Q.TaskStatusId,
                    TaskType = Q.TaskType.Name,
                    TaskStatus = Q.TaskStatus.Name,
                    Title = Q.Title,
                    De
                })
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return res;
        }
    }
}
