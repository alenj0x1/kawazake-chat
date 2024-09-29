using System;
using System.Collections.Generic;

namespace KawasakeChat.Domain.Entities;

public partial class Groupchatmember
{
    public Guid GroupId { get; set; }

    public Guid UserId { get; set; }

    public Guid MemberId { get; set; }

    public int Role { get; set; }

    public DateTime JoinedAt { get; set; }

    public virtual Groupchat Group { get; set; } = null!;

    public virtual ICollection<Groupchatmessage> Groupchatmessages { get; set; } = new List<Groupchatmessage>();

    public virtual Groupchatmemberrole RoleNavigation { get; set; } = null!;

    public virtual Useraccount User { get; set; } = null!;
}
