using System;
using System.Collections.Generic;

namespace Task.API.Entities;

public partial class TaskManagement
{
    public Guid TaskId { get; set; }

    public Guid UserId { get; set; }

    public int TaskTypeId { get; set; }

    public int TaskStatusId { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public DateTime PlannedStart { get; set; }

    public DateTime PlannedEnd { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual TaskStatus TaskStatus { get; set; } = null!;

    public virtual TaskType TaskType { get; set; } = null!;
}
