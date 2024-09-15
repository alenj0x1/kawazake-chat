using System;
using System.Collections.Generic;

namespace KawasakeChat.Domain.Entities;

public partial class Groupchatmember
{
    public Guid GroupId { get; set; }
    public Guid MemberId { get; set; }
    public int Role { get; set; }
    public DateTime JoinedAt { get; set; }
    public virtual Groupchat Group { get; set; } = null!;
    public virtual Useraccount Member { get; set; } = null!;
    public virtual Groupchatmemberrole RoleNavigation { get; set; } = null!;
}
