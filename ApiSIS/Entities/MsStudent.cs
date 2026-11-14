using System;
using System.Collections.Generic;

namespace ApiSIS.Entities;

public partial class MsStudent
{
    public int Id { get; set; }

    public string Nis { get; set; } = null!;

    public string? Nisn { get; set; }

    public string StudentName { get; set; } = null!;

    public string? Gender { get; set; }

    public DateOnly? BirthDate { get; set; }

    public string? Address { get; set; }

    public string? ParentName { get; set; }

    public string? ParentPhone { get; set; }

    public bool? Status { get; set; }

    public DateTime? CreatedDate { get; set; }

    public virtual ICollection<AbsensiSiswa> AbsensiSiswas { get; set; } = new List<AbsensiSiswa>();

    public virtual ICollection<IzinSiswa> IzinSiswas { get; set; } = new List<IzinSiswa>();

    public virtual ICollection<RekapAbsensi> RekapAbsensis { get; set; } = new List<RekapAbsensi>();
}
