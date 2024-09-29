using System;
using System.Collections.Generic;

namespace KawasakeChat.Domain.Entities;

public partial class Tokenrefresh
{
    public int TokenRefreshId { get; set; }

    public Guid UserId { get; set; }

    public string Value { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime? Expiration { get; set; }

    public virtual ICollection<Tokenaccess> Tokenaccesses { get; set; } = new List<Tokenaccess>();

    public virtual Useraccount User { get; set; } = null!;
}
