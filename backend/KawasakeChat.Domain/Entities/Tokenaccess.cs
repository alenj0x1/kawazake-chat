using System;
using System.Collections.Generic;

namespace KawasakeChat.Domain.Entities;

public partial class Tokenaccess
{
    public int TokenAccessId { get; set; }

    public int TokenRefreshId { get; set; }

    public Guid UserId { get; set; }

    public string Value { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime Expiration { get; set; }

    public virtual Tokenrefresh TokenRefresh { get; set; } = null!;

    public virtual Useraccount User { get; set; } = null!;
}
