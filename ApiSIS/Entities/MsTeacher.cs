using System;
using System.Collections.Generic;

namespace ApiSIS.Entities;

public partial class MsTeacher
{
    public int Id { get; set; }

    public string? Nip { get; set; }

    public string? TeacherName { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public bool? Status { get; set; }

    public virtual ICollection<IzinSiswa> IzinSiswas { get; set; } = new List<IzinSiswa>();

    public virtual ICollection<JadwalPelajaran> JadwalPelajarans { get; set; } = new List<JadwalPelajaran>();
}
