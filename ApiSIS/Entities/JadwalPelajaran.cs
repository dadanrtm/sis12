using System;
using System.Collections.Generic;

namespace ApiSIS.Entities;

public partial class JadwalPelajaran
{
    public int IdJadwal { get; set; }

    public int IdClass { get; set; }

    public int IdTeacher { get; set; }

    public string MataPelajaran { get; set; } = null!;

    public string Hari { get; set; } = null!;

    public TimeOnly JamMulai { get; set; }

    public TimeOnly JamSelesai { get; set; }

    public string? Ruangan { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual MsClass IdClassNavigation { get; set; } = null!;

    public virtual MsTeacher IdTeacherNavigation { get; set; } = null!;
}
