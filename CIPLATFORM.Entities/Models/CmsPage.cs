using System;
using System.Collections.Generic;

namespace CIPLATFORM.Entities.Models;

public partial class CmsPage
{
    public long CmsPageId { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Slug { get; set; } = null!;

    public int Status { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }
}
