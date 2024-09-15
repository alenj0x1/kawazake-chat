using System;
using System.Collections.Generic;

namespace KawasakeChat.Domain.Entities;

public partial class Useraccount
{
    public Guid UserId { get; set; }
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string? Status { get; set; }
    public int Role { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    public virtual ICollection<Groupchatmessage> Groupchatmessages { get; set; } = new List<Groupchatmessage>();
    public virtual Useraccountrole RoleNavigation { get; set; } = null!;
}
