using System;
using System.Collections.Generic;

namespace CIPLATFORM.Entities.Models;

public partial class PasswordReset
{
    public long Id { get; set; }

    public string Email { get; set; } = null!;

    public string Token { get; set; } = null!;

    public DateTime CreatedAt { get; set; }
}
