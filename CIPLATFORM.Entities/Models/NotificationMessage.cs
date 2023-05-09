using System;
using System.Collections.Generic;

namespace CIPLATFORM.Entities.Models;

public partial class NotificationMessage
{
    public long NotificationMessageId { get; set; }

    public long UserId { get; set; }

    public string Message { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public string Type { get; set; } = null!;

    public long Id { get; set; }

    public string Status { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
