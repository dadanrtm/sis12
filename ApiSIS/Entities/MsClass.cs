using System;
using System.Collections.Generic;

namespace ApiSIS.Entities;

public partial class MsClass
{
    public int Id { get; set; }

    public string? ClassName { get; set; }

    public int? GradeLevel { get; set; }

    public int? HomeroomTeacherId { get; set; }

    public virtual ICollection<JadwalPelajaran> JadwalPelajarans { get; set; } = new List<JadwalPelajaran>();
}
