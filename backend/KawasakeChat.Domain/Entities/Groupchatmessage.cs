using System;
using System.Collections.Generic;

namespace KawasakeChat.Domain.Entities;

public partial class Groupchatmessage
{
    public Guid GroupId { get; set; }
    public Guid MemberId { get; set; }
    public int MessageId { get; set; }
    public string Content { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public virtual Groupchat Group { get; set; } = null!;
    public virtual Useraccount Member { get; set; } = null!;
}
