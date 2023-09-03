using System.ComponentModel.DataAnnotations;

namespace Task.API.Models.Task
{
    public class CreateUpdateTaskDto
    {
        [Required]
        public int TaskTypeId { get; set; }

        [Required]
        public int TaskStatusId { get; set; }

        [Required]
        public string Title { get; set; } = null!;
        [Required]
        public string Description { get; set; } = null!;
        [Required]
        public DateTime PlannedStart { get; set; }
        [Required]

        public DateTime PlannedEnd { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}
