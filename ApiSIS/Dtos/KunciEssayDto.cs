namespace ApiSIS.Dtos;

// Untuk RESPONSE (data keluar dari API)
public class KunciEssayDto
{
    public string? KunciJawaban { get; set; }
    public string? RubrikPenilaian { get; set; }
}

// Untuk REQUEST (data masuk dari client saat CREATE soal essay)
public class KunciEssayCreateDto
{
    public string? KunciJawaban { get; set; }
    public string? RubrikPenilaian { get; set; }
}

// Untuk REQUEST (saat UPDATE kunci essay saja, tanpa bikin soal baru)
public class KunciEssayUpdateDto
{
    public string? KunciJawaban { get; set; }
    public string? RubrikPenilaian { get; set; }
}
