using Library.Backend.Helpers;
using Microsoft.EntityFrameworkCore;
using Task.API.Entities;
using Task.API.Models.Task;
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
            var res = await _context.TaskManagements
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
                    Description = Q.Description,
                })
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return res;
        }

        public async Task<List<TaskDto>> GetListAsync(Guid userId, CancellationToken cancellationToken)
        {
            var tasks = await _context.TaskManagements
                .Where(Q => Q.UserId == userId)
                .AsNoTracking()
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
                    Description = Q.Description,
                    TaskTypeId = Q.TaskTypeId
                })
                .ToListAsync(cancellationToken);

            return tasks;
        }

        public async Task<(ValidationError? error, TaskDto? task)> GetAsync(Guid userId, Guid taskId, CancellationToken cancellationToken)
        {
            var task = await _context.TaskManagements
                .Include(Q=>Q.TaskStatus)
                .Include(Q=>Q.TaskType)
                .Where(Q => Q.TaskId == taskId)                
                .AsNoTracking()
                .FirstOrDefaultAsync(cancellationToken);

            if (task == null)
            {
                return (new ValidationError
                {
                    Field = nameof(taskId),
                    Message = "Task not found",
                }, null);
            }

            if (task.UserId != userId)
            {
                return (new ValidationError
                {
                    Field = nameof(taskId),
                    Message = "Your not authorize for the data",
                }, null);
            }

            var detail = new TaskDto
            {
                CreatedAt = task.CreatedAt,
                EndDate = task.EndDate,
                PlannedEnd = task.PlannedEnd,
                PlannedStart = task.PlannedStart,
                StartDate = task.StartDate,
                TaskId = task.TaskId,
                TaskStatusId = task.TaskStatusId,
                TaskTypeId = task.TaskTypeId,
                TaskType = task.TaskType.Name,
                TaskStatus = task.TaskStatus.Name,
                Title = task.Title,
                Description = task.Description,
            };

            return (null, detail);
        }

        public async Task<ValidationError> CreateAsync(Guid userId, CreateUpdateTaskDto dto, CancellationToken cancellationToken)
        {
            var status = await _context.TaskStatuses
                .Where(Q => Q.TaskStatusId == dto.TaskStatusId)
                .Select(Q => new
                {
                    Q.Name
                })
                .FirstOrDefaultAsync(cancellationToken);

            if (status == null)
            {
                return new ValidationError
                {
                    Field = nameof(dto.TaskStatusId),
                    Message = "Status not found",
                };
            }

            var typeAny = await _context.TaskTypes
                .AnyAsync(Q => Q.TaskTypeId == dto.TaskTypeId, cancellationToken);

            if (!typeAny)
            {
                return new ValidationError
                {
                    Field = nameof(dto.TaskStatusId),
                    Message = "Type not found",
                };
            }

            var allowedRoles = new List<int>
            {
                (int)Tasks.API.Enums.TaskStatus.Created,
                (int)Tasks.API.Enums.TaskStatus.InProgress,
                (int)Tasks.API.Enums.TaskStatus.Finish,
            };

            if (!allowedRoles.Contains(dto.TaskStatusId))
            {
                return new ValidationError
                {
                    Field = nameof(dto.TaskStatusId),
                    Message = $"Cannot create task with status {status.Name} on create process",
                };
            }

            if (dto.TaskStatusId == (int)Tasks.API.Enums.TaskStatus.InProgress && dto.StartDate == null)
            {
                return new ValidationError
                {
                    Field = nameof(dto.TaskStatusId),
                    Message = $"Start Date must be filled",
                };
            }

            if (dto.TaskStatusId == (int)Tasks.API.Enums.TaskStatus.Finish && (dto.StartDate == null || dto.EndDate == null))
            {
                return new ValidationError
                {
                    Field = nameof(dto.TaskStatusId),
                    Message = $"Start Date and End Date must be filled",
                };
            }

            var task = new TaskManagement
            {
                CreatedAt = DateTime.UtcNow,
                Description = dto.Description,
                EndDate = dto.EndDate,
                PlannedEnd = dto.PlannedEnd,
                PlannedStart = dto.PlannedStart,
                StartDate = dto.StartDate,
                TaskId = Guid.NewGuid(),
                TaskStatusId = dto.TaskStatusId,
                TaskTypeId = dto.TaskTypeId,
                Title = dto.Title,
                UserId = userId,
            };

            _context.TaskManagements.Add(task);
            await _context.SaveChangesAsync(cancellationToken);
            return null;
        }

        public async Task<ValidationError> UpdateAsync(Guid userId, Guid taskId, CreateUpdateTaskDto dto, CancellationToken cancellationToken)
        {
            var task = await _context.TaskManagements
                .Where(Q => Q.TaskId == taskId)
                .FirstOrDefaultAsync(cancellationToken);

            if (task == null)
            {
                return new ValidationError
                {
                    Field = nameof(taskId),
                    Message = "Task not found",
                };
            }

            var status = await _context.TaskStatuses
                .Where(Q => Q.TaskStatusId == dto.TaskStatusId)
                .Select(Q => new
                {
                    Q.Name
                })
                .FirstOrDefaultAsync(cancellationToken);

            if (status == null)
            {
                return new ValidationError
                {
                    Field = nameof(dto.TaskStatusId),
                    Message = "Status not found",
                };
            }

            var typeAny = await _context.TaskTypes
                .AnyAsync(Q => Q.TaskTypeId == dto.TaskTypeId, cancellationToken);

            if (!typeAny)
            {
                return new ValidationError
                {
                    Field = nameof(dto.TaskStatusId),
                    Message = "Type not found",
                };
            }

            var allowedRoles = new List<int>
            {
                (int)Tasks.API.Enums.TaskStatus.Reschedule,
                (int)Tasks.API.Enums.TaskStatus.Cancel,
                (int)Tasks.API.Enums.TaskStatus.InProgress,
                (int)Tasks.API.Enums.TaskStatus.Finish,
            };

            if (!allowedRoles.Contains(dto.TaskStatusId))
            {
                return new ValidationError
                {
                    Field = nameof(dto.TaskStatusId),
                    Message = $"Cannot create task with status {status.Name} on create process",
                };
            }

            if (dto.TaskStatusId == (int)Tasks.API.Enums.TaskStatus.InProgress && dto.StartDate == null)
            {
                return new ValidationError
                {
                    Field = nameof(dto.TaskStatusId),
                    Message = $"Start Date must be filled",
                };
            }

            if (dto.TaskStatusId == (int)Tasks.API.Enums.TaskStatus.Finish && (dto.StartDate == null || dto.EndDate == null))
            {
                return new ValidationError
                {
                    Field = nameof(dto.TaskStatusId),
                    Message = $"Start Date and End Date must be filled",
                };
            }

            task.Description = dto.Description;
            task.EndDate = dto.EndDate;
            task.PlannedEnd = dto.PlannedEnd;
            task.PlannedStart = dto.PlannedStart;
            task.StartDate = dto.StartDate;
            task.TaskStatusId = dto.TaskStatusId;
            task.TaskTypeId = dto.TaskTypeId;
            task.Title = dto.Title;
            task.UserId = userId;

            _context.TaskManagements.Update(task);
            await _context.SaveChangesAsync(cancellationToken);
            return null;
        }

        public async Task<ValidationError> DeleteAsync(Guid userId, Guid taskId, CancellationToken cancellationToken)
        {
            var task = await _context.TaskManagements
                .Where(Q => Q.TaskId == taskId)
                .FirstOrDefaultAsync(cancellationToken);

            if (task == null)
            {
                return new ValidationError
                {
                    Field = nameof(taskId),
                    Message = "Task not found",
                };
            }

            _context.Remove(task);
            await _context.SaveChangesAsync(cancellationToken);

            return null;
        }
    }
}
