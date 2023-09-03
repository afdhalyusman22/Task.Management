using Task.API.Entities;
using Microsoft.EntityFrameworkCore;
using Task.API.Models.TaskStatus;
using Task.API.Service.Interfaces;

namespace Task.API.Service
{
    public class TaskStatusService : ITaskStatusService
    {
        private readonly DataContext _context;

        public TaskStatusService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<TaskStatusDto>> GetListAsync(CancellationToken cancellationToken)
        {
            var res = await _context.TaskStatuses
                .Select(Q => new TaskStatusDto
                {
                    Id = Q.TaskStatusId,
                    Name = Q.Name,
                })
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return res;
        }
    }

}
