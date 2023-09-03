using Library.Backend.Helpers;

namespace Task.API.Models.TaskType
{
    public class TaskTypeDto : BaseDtoId<int>
    {
        public string Name { get; set; } = string.Empty;
    }
}
