using System;
using System.Collections.Generic;

namespace ApiSIS.Entities;

public partial class Ujian
{
    public int Id { get; set; }

    public string NamaUjian { get; set; } = null!;

    public int MataPelajaranId { get; set; }

    public int? DurasiMenit { get; set; }

    public DateTime? TanggalMulai { get; set; }

    public DateTime? TanggalSelesai { get; set; }

    public string? Status { get; set; }

    public virtual ICollection<JawabanSiswa> JawabanSiswas { get; set; } = new List<JawabanSiswa>();

    public virtual MataPelajaran MataPelajaran { get; set; } = null!;

    public virtual ICollection<UjianSoal> UjianSoals { get; set; } = new List<UjianSoal>();
}
