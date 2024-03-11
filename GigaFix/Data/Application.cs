using System;
using System.Collections.Generic;

namespace GigaFix.Data;

public partial class Application
{
    public int IdApplication { get; set; }

    public DateOnly? DateAddition { get; set; }

    public string? NameEquipment { get; set; }

    public int? IdTypeProblem { get; set; }

    public string? Comment { get; set; }

    public string? Status { get; set; }

    public string? NameClient { get; set; }

    public decimal? Cost { get; set; }

    public DateOnly? DateEnd { get; set; }

    public TimeOnly? TimeWork { get; set; }

    public int? IdEmployee { get; set; }

    public string? WorkStatus { get; set; }

    /// <summary>
    /// предварительная дата завершения
    /// </summary>
    public DateOnly? PeriodExecution { get; set; }

    public int? IdTypeEquipment { get; set; }

    /// <summary>
    /// серийный номер
    /// </summary>
    public int? Number { get; set; }

    public string? Description { get; set; }

    public virtual User? IdEmployeeNavigation { get; set; }

    public virtual TypeEquipment? IdTypeEquipmentNavigation { get; set; }

    public virtual TypeProblem? IdTypeProblemNavigation { get; set; }

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();
}
