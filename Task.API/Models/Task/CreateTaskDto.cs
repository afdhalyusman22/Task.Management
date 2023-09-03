namespace Task.API.Models.Task
{
    public class CreateUpdateTaskDto
    {
        public int TaskTypeId { get; set; }

        public int TaskStatusId { get; set; }

        public string Title { get; set; } = null!;

        public DateTime PlannedStart { get; set; }

        public DateTime PlannedEnd { get; set; }
    }
}
