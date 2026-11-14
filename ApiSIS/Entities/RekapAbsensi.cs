using System;
using System.Collections.Generic;

namespace ApiSIS.Entities;

public partial class RekapAbsensi
{
    public int IdRekap { get; set; }

    public int IdStudent { get; set; }

    public int Bulan { get; set; }

    public int Tahun { get; set; }

    public int? Hadir { get; set; }

    public int? Izin { get; set; }

    public int? Sakit { get; set; }

    public int? Alfa { get; set; }

    public int? Terlambat { get; set; }

    public int? TotalHariEfektif { get; set; }

    public decimal? PersentaseKehadiran { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual MsStudent IdStudentNavigation { get; set; } = null!;
}
