using System;
using System.Collections.Generic;

namespace ApiSIS.Entities;

public partial class AbsensiSiswa
{
    public int IdAbsensi { get; set; }

    public int IdStudent { get; set; }

    public DateOnly Tanggal { get; set; }

    public TimeOnly? JamDatang { get; set; }

    public TimeOnly? JamPulang { get; set; }

    public string? Status { get; set; }

    public string? Keterangan { get; set; }

    public string? FotoDatang { get; set; }

    public string? FotoPulang { get; set; }

    public string? LokasiDatang { get; set; }

    public string? LokasiPulang { get; set; }

    public decimal? LatitudeDatang { get; set; }

    public decimal? LongitudeDatang { get; set; }

    public decimal? LatitudePulang { get; set; }

    public decimal? LongitudePulang { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual MsUser? CreatedByNavigation { get; set; }

    public virtual MsStudent IdStudentNavigation { get; set; } = null!;
}
