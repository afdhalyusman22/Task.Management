using Library.Backend.Helpers;

namespace Task.API.Models.TaskStatus
{
    public class TaskStatusDto : BaseDtoId<int>
    {
        public string Name { get; set; } = string.Empty;
    }
}
