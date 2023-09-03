namespace Task.API.Models.Task
{
    public class TaskDto
    {
        public Guid TaskId { get; set; }

        public Guid UserId { get; set; }

        public int TaskTypeId { get; set; }
        public string TaskType { get; set; } = null!;

        public int TaskStatusId { get; set; }
        public string TaskStatus { get; set; } = null!;

        public string Title { get; set; } = null!;

        public DateTime PlannedStart { get; set; }

        public DateTime PlannedEnd { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
