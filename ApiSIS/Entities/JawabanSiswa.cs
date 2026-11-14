using System;
using System.Collections.Generic;

namespace ApiSIS.Entities;

public partial class JawabanSiswa
{
    public int Id { get; set; }

    public int UjianId { get; set; }

    public int BankSoalId { get; set; }

    public int SiswaId { get; set; }

    public int? PilihanJawabanId { get; set; }

    public string? JawabanEssay { get; set; }

    public string? GambarJawaban { get; set; }

    public decimal? Nilai { get; set; }

    public virtual BankSoal BankSoal { get; set; } = null!;

    public virtual PilihanJawaban? PilihanJawaban { get; set; }

    public virtual Ujian Ujian { get; set; } = null!;
}
