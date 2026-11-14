namespace ApiSIS.DTOs;

 public class PilihanJawabanDto
{
    public int Id { get; set; }
    public string Label { get; set; } = null!;
    public string? TeksJawaban { get; set; }
    public string? GambarJawaban { get; set; }
    public bool? IsBenar { get; set; }
}

public class PilihanJawabanCreateDto
{
    public string Label { get; set; } = null!;
    public string? TeksJawaban { get; set; }
    public string? GambarJawaban { get; set; }
    public bool? IsBenar { get; set; }

}




