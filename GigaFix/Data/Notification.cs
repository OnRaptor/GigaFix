using System;
using System.Collections.Generic;

namespace GigaFix.Data;

public partial class Notification
{
    public int UserId { get; set; }

    public string Message { get; set; } = null!;

    public int? ApplicationId { get; set; }

    public int Id { get; set; }

    public virtual Application? Application { get; set; }

    public virtual User User { get; set; } = null!;
}
