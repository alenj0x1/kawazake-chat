using System;
using System.Collections.Generic;

namespace KawasakeChat.Domain.Entities;

public partial class Useraccountrole
{
    public int RoleId { get; set; }
    public string Name { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    public virtual ICollection<Useraccount> Useraccounts { get; set; } = new List<Useraccount>();
}
