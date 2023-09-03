using System;
using System.Collections.Generic;

namespace User.API.Entities;

public partial class UserAccount
{
    public Guid UserId { get; set; }

    public string Email { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    public int GenderId { get; set; }

    public DateTime LastLogin { get; set; }

    public DateTime RegisterAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual Gender Gender { get; set; } = null!;
}
