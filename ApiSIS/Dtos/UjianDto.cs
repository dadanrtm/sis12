namespace ApiSIS.DTOs;
public class UjianDto
{
    public int Id { get; set; }
    public string NamaUjian { get; set; } = null!;
    public int MataPelajaranId { get; set; }
    public string NamaMapel { get; set; } = null!;
    public int DurasiMenit { get; set; }
    public DateTime? TanggalMulai { get; set; }
    public DateTime? TanggalSelesai { get; set; }
    public string Status { get; set; } = null!;
    public int JumlahSoal { get; set; }
}

public class GenerateSoalDto
{
    public int JumlahMc { get; set; }
    public int JumlahEssay { get; set; }
    public int? MataPelajaranId { get; set; }
}

public class SubmitJawabanDto
{
    public int UjianId { get; set; }
    public int BankSoalId { get; set; }
    public int SiswaId { get; set; }
    public int? PilihanJawabanId { get; set; }
    public string? JawabanEssay { get; set; }
    public string? GambarJawaban { get; set; }
}

public class NilaiAkhirDto
{
    public int SiswaId { get; set; }
    public int UjianId { get; set; }
    public decimal TotalNilaiDidapat { get; set; }
    public decimal TotalBobotMaksimal { get; set; }
    public decimal NilaiAkhir100 { get; set; }
}