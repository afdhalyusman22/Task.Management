using System;
using System.Collections.Generic;

namespace Task.API.Entities;

public partial class TaskStatus
{
    public int TaskStatusId { get; set; }

    public string Name { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();
}
