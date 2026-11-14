using System;
using System.Collections.Generic;

namespace ApiSIS.Entities;

public partial class IzinSiswa
{
    public int IdIzin { get; set; }

    public int IdStudent { get; set; }

    public string JenisIzin { get; set; } = null!;

    public DateOnly TanggalMulai { get; set; }

    public DateOnly TanggalAkhir { get; set; }

    public string Alasan { get; set; } = null!;

    public string? SuratKeterangan { get; set; }

    public string? Status { get; set; }

    public int? ApprovedBy { get; set; }

    public DateTime? ApprovedAt { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual MsTeacher? ApprovedByNavigation { get; set; }

    public virtual MsStudent IdStudentNavigation { get; set; } = null!;
}
