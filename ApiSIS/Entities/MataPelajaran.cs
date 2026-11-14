using System;
using System.Collections.Generic;

namespace ApiSIS.Entities;

public partial class MataPelajaran
{
    public int Id { get; set; }

    public string Kode { get; set; } = null!;

    public string Nama { get; set; } = null!;

    public string? Deskripsi { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<BankSoal> BankSoals { get; set; } = new List<BankSoal>();

    public virtual ICollection<Ujian> Ujians { get; set; } = new List<Ujian>();
}
