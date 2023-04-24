using System;
using System.Collections.Generic;

namespace CIPLATFORM.Entities.Models;

public partial class StoryView
{
    public long StoryViewId { get; set; }

    public long StoryId { get; set; }

    public long UserId { get; set; }

    public virtual Story Story { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
