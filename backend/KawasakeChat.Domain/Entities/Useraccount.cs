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

    public virtual Groupchatmember? Groupchatmember { get; set; }

    public virtual Useraccountrole RoleNavigation { get; set; } = null!;

    public virtual ICollection<Tokenaccess> Tokenaccesses { get; set; } = new List<Tokenaccess>();

    public virtual ICollection<Tokenrefresh> Tokenrefreshes { get; set; } = new List<Tokenrefresh>();
}
