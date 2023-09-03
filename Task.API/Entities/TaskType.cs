using System;
using System.Collections.Generic;

namespace Task.API.Entities;

public partial class TaskType
{
    public int TaskTypeId { get; set; }

    public string Name { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();
}
