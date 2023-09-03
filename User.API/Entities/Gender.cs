using System;
using System.Collections.Generic;

namespace User.API.Entities;

public partial class Gender
{
    public int GenderId { get; set; }

    public string Name { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<UserAccount> UserAccounts { get; set; } = new List<UserAccount>();
}
