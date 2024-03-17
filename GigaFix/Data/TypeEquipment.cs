using System.Collections.Generic;

namespace GigaFix.Data;

public partial class TypeEquipment
{
    public int IdTypeEquipment { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Application> Applications { get; set; } = new List<Application>();
}