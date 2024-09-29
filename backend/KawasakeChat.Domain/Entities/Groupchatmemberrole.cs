using System;
using System.Collections.Generic;

namespace KawasakeChat.Domain.Entities;

public partial class Groupchatmemberrole
{
    public int RoleId { get; set; }

    public string? Name { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual ICollection<Groupchatmember> Groupchatmembers { get; set; } = new List<Groupchatmember>();
}
