using System;
using System.Collections.Generic;

namespace ApiSIS.Entities;

public partial class PilihanJawaban
{
    public int Id { get; set; }

    public int BankSoalId { get; set; }

    public string Label { get; set; } = null!;

    public string? TeksJawaban { get; set; }

    public string? GambarJawaban { get; set; }

    public bool? IsBenar { get; set; }

    public virtual BankSoal BankSoal { get; set; } = null!;

    public virtual ICollection<JawabanSiswa> JawabanSiswas { get; set; } = new List<JawabanSiswa>();
}
