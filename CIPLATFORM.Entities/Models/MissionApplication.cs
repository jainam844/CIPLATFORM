﻿using System;
using System.Collections.Generic;

namespace CIPLATFORM.Entities.Models;

public partial class MissionApplication
{
    public long MissionApplicationId { get; set; }

    public long MissionId { get; set; }

    public long UserId { get; set; }

    public DateTime AppliedAt { get; set; }

    public string ApprovalStatus { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual Mission Mission { get; set; } = null!;

    public virtual ICollection<NotificationPreference> NotificationPreferences { get; } = new List<NotificationPreference>();

    public virtual User User { get; set; } = null!;
}
