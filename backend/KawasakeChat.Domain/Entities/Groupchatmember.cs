using System;
using System.Collections.Generic;

namespace KawasakeChat.Domain.Entities;

public partial class Groupchatmember
{
    public int GroupChatMemberId { get; set; }

    public Guid GroupChatId { get; set; }

    public Guid UserId { get; set; }

    public Guid MemberId { get; set; }

    public string? AvatarUrl { get; set; }

    public int Role { get; set; }

    public Guid? RoleGrantedBy { get; set; }

    public DateTime JoinedAt { get; set; }

    public virtual Groupchat GroupChat { get; set; } = null!;

    public virtual ICollection<Groupchatmessage> Groupchatmessages { get; set; } = new List<Groupchatmessage>();

    public virtual ICollection<Groupchatmember> InverseRoleGrantedByNavigation { get; set; } = new List<Groupchatmember>();

    public virtual Groupchatmember? RoleGrantedByNavigation { get; set; }

    public virtual Groupchatmemberrole RoleNavigation { get; set; } = null!;

    public virtual Useraccount User { get; set; } = null!;
}
