using System;
using System.Collections.Generic;

namespace CIPLATFORM.Entities.Models;

public partial class Notification
{
    public long NotificationId { get; set; }

    public long UserId { get; set; }

    public string Message { get; set; } = null!;

    public int Status { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual ICollection<NotificationPreference> NotificationPreferences { get; } = new List<NotificationPreference>();

    public virtual User User { get; set; } = null!;
}
