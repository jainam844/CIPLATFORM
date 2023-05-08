using System;
using System.Collections.Generic;

namespace CIPLATFORM.Entities.Models;

public partial class NotificationPreference
{
    public long NotificationPreferencesId { get; set; }

    public long NotificationId { get; set; }

    public long UserId { get; set; }

    public long? RecommendedMission { get; set; }

    public TimeSpan? VolunterringHours { get; set; }

    public int? VolunterringGoals { get; set; }

    public long? MyComments { get; set; }

    public long? MyStory { get; set; }

    public long? NewMission { get; set; }

    public long? RecommendStory { get; set; }

    public long? MissionApplication { get; set; }

    public string? Mail { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual MissionApplication? MissionApplicationNavigation { get; set; }

    public virtual Comment? MyCommentsNavigation { get; set; }

    public virtual Story? MyStoryNavigation { get; set; }

    public virtual Mission? NewMissionNavigation { get; set; }

    public virtual Notification Notification { get; set; } = null!;

    public virtual Story? RecommendStoryNavigation { get; set; }

    public virtual Mission? RecommendedMissionNavigation { get; set; }

    public virtual User User { get; set; } = null!;
}
