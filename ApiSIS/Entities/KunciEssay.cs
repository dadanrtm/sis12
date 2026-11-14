using System;
using System.Collections.Generic;

namespace ApiSIS.Entities;

public partial class KunciEssay
{
    public int Id { get; set; }

    public int BankSoalId { get; set; }

    public string? KunciJawaban { get; set; }

    public string? RubrikPenilaian { get; set; }

    public virtual BankSoal BankSoal { get; set; } = null!;
}
