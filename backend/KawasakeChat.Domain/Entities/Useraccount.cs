using System;
using System.Collections.Generic;

namespace KawasakeChat.Domain.Entities;

public partial class Useraccount
{
    public Guid UserId { get; set; }

    public string Username { get; set; } = null!;

    public string? AvatarUrl { get; set; }

    public string Password { get; set; } = null!;

    public string? Status { get; set; }

    public int Role { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual ICollection<Groupchatmember> Groupchatmembers { get; set; } = new List<Groupchatmember>();

    public virtual ICollection<Groupchat> Groupchats { get; set; } = new List<Groupchat>();

    public virtual Useraccountrole RoleNavigation { get; set; } = null!;

    public virtual ICollection<Tokenaccess> Tokenaccesses { get; set; } = new List<Tokenaccess>();

    public virtual ICollection<Tokenrefresh> Tokenrefreshes { get; set; } = new List<Tokenrefresh>();
}
