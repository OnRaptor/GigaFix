using System;
using System.Collections.Generic;

namespace GigaFix.Data;

public partial class User
{
    public int IdUser { get; set; }

    public string? Login { get; set; }

    public string? Password { get; set; }

    public string? FullnameUser { get; set; }

    public string? Role { get; set; }

    public virtual ICollection<Application> Applications { get; set; } = new List<Application>();

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();
}
