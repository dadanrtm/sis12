


using ApiSIS.Dtos;

namespace ApiSIS.DTOs;

public class BankSoalDto
{
    public int Id { get; set; }
    public int MataPelajaranId { get; set; }
    public string NamaMapel { get; set; } = null!;
    public string TipeSoal { get; set; } = null!;
    public string Pertanyaan { get; set; } = null!;
    public string? GambarSoal { get; set; }
    public string TingkatKesulitan { get; set; } = null!;
    public int? BobotNilai { get; set; }
    public string? Pembahasan { get; set; }
    public string Status { get; set; } = null!;
}

public class BankSoalDetailDto : BankSoalDto
{
    public List<PilihanJawabanDto> PilihanJawaban { get; set; } = new();
    public KunciEssayDto? KunciEssay { get; set; }
}

public class BankSoalCreateDto
{
    public int MataPelajaranId { get; set; }
    public string TipeSoal { get; set; } = null!;
    public string Pertanyaan { get; set; } = null!;
    public string? GambarSoal { get; set; }
    public string TingkatKesulitan { get; set; } = "sedang";
    public int BobotNilai { get; set; } = 1;
    public string? Pembahasan { get; set; }
    public int? CreatedBy { get; set; }
    public List<PilihanJawabanCreateDto>? PilihanJawaban { get; set; }
    public KunciEssayCreateDto? KunciEssay { get; set; }
}

public class BankSoalUpdateDto
{
    public int? MataPelajaranId { get; set; }
    public string? Pertanyaan { get; set; }
    public string? GambarSoal { get; set; }
    public string? TingkatKesulitan { get; set; }
    public int? BobotNilai { get; set; }
    public string? Pembahasan { get; set; }
    public string? Status { get; set; }
}

public class BankSoalFilterDto
{
    public int? MataPelajaranId { get; set; }
    public string? TipeSoal { get; set; }
    public string? TingkatKesulitan { get; set; }
    public string Status { get; set; } = "aktif";
}