using System;
using System.Collections.Generic;

namespace KawasakeChat.Domain.Entities;

public partial class Groupchatmessage
{
    public Guid GroupChatId { get; set; }

    public Guid MemberId { get; set; }

    public int MessageId { get; set; }

    public string Content { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual Groupchat GroupChat { get; set; } = null!;

    public virtual Groupchatmember Member { get; set; } = null!;
}
