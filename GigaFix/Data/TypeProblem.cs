using System.Collections.Generic;

namespace GigaFix.Data;

public partial class TypeProblem
{
    /// <summary>
    /// тип неисправностей
    /// </summary>
    public int IdTypeProblemt { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Application> Applications { get; set; } = new List<Application>();
}