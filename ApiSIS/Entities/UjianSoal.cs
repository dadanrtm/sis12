using System;
using System.Collections.Generic;

namespace ApiSIS.Entities;

public partial class UjianSoal
{
    public int Id { get; set; }

    public int UjianId { get; set; }

    public int BankSoalId { get; set; }

    public int? Urutan { get; set; }

    public virtual BankSoal BankSoal { get; set; } = null!;

    public virtual Ujian Ujian { get; set; } = null!;
}
