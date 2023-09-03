using Microsoft.EntityFrameworkCore;
using Task.API.Entities;
using Task.API.Models.TaskType;
using Task.API.Service.Interfaces;

namespace Task.API.Service
{
    public class TaskTypeService : ITaskTypeService
    {
        private readonly DataContext _context;

        public TaskTypeService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<TaskTypeDto>> GetListAsync(CancellationToken cancellationToken)
        {
            var res = await _context.TaskTypes
                .Select(Q => new TaskTypeDto
                {
                    Id = Q.TaskTypeId,
                    Name = Q.Name,
                })
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return res;
        }
    }
}
