using System;
using System.Collections.Generic;

namespace ApiSIS.Entities;

public partial class BankSoal
{
    public int Id { get; set; }

    public int MataPelajaranId { get; set; }

    public string TipeSoal { get; set; } = null!;

    public string Pertanyaan { get; set; } = null!;

    public string? GambarSoal { get; set; }

    public string? TingkatKesulitan { get; set; }

    public int? BobotNilai { get; set; }

    public string? Pembahasan { get; set; }

    public string? Status { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<JawabanSiswa> JawabanSiswas { get; set; } = new List<JawabanSiswa>();

    public virtual KunciEssay? KunciEssay { get; set; }

    public virtual MataPelajaran MataPelajaran { get; set; } = null!;

    public virtual ICollection<PilihanJawaban> PilihanJawabans { get; set; } = new List<PilihanJawaban>();

    public virtual ICollection<UjianSoal> UjianSoals { get; set; } = new List<UjianSoal>();
}
