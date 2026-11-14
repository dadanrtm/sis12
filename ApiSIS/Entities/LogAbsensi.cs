using System;
using System.Collections.Generic;

namespace ApiSIS.Entities;

public partial class LogAbsensi
{
    public int IdLog { get; set; }

    public int? IdAbsensi { get; set; }

    public string? Action { get; set; }

    public string? OldData { get; set; }

    public string? NewData { get; set; }

    public int? ChangedBy { get; set; }

    public DateTime? ChangedAt { get; set; }

    public virtual MsUser? ChangedByNavigation { get; set; }
}
