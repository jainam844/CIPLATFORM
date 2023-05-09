using System;
using System.Collections.Generic;

namespace CIPLATFORM.Entities.Models;

public partial class NotificationSetting
{
    public long NotificationId { get; set; }

    public long UserId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public bool? RecommendedMission { get; set; }

    public bool? Story { get; set; }

    public bool? NewMission { get; set; }

    public bool? RecommendedStory { get; set; }

    public bool? MissionApplication { get; set; }

    public bool EmailNotification { get; set; }

    public virtual User User { get; set; } = null!;
}
