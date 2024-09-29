using System;
using System.Collections.Generic;

namespace KawasakeChat.Domain.Entities;

public partial class Groupchat
{
    public Guid GroupId { get; set; }

    public string Name { get; set; } = null!;

    public string InviteCode { get; set; } = null!;

    public bool Private { get; set; }

    public string? Password { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual ICollection<Groupchatmember> Groupchatmembers { get; set; } = new List<Groupchatmember>();

    public virtual ICollection<Groupchatmessage> Groupchatmessages { get; set; } = new List<Groupchatmessage>();
}
