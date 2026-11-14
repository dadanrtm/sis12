using ApiSIS.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace ApiSIS.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PilihanJawabanController : ControllerBase
{
    private readonly AppDbContext _context;

    public PilihanJawabanController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("by-soal/{bankSoalId}")]
    public async Task<IActionResult> GetBySoal(int bankSoalId)
    {
        var data = await _context.PilihanJawabans
            .Where(x => x.BankSoalId == bankSoalId)
            .OrderBy(x => x.Label)
            .Select(x => new { x.Id, x.Label, x.TeksJawaban, x.GambarJawaban, x.IsBenar })
            .ToListAsync();
        return Ok(data);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePilihanDto dto)
    {
        try
        {
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC sp_PilihanJawaban_Insert @BankSoalId, @Label, @TeksJawaban, @GambarJawaban, @IsBenar",
                new SqlParameter("@BankSoalId", dto.BankSoalId),
                new SqlParameter("@Label", dto.Label),
                new SqlParameter("@TeksJawaban", (object?)dto.TeksJawaban ?? DBNull.Value),
                new SqlParameter("@GambarJawaban", (object?)dto.GambarJawaban ?? DBNull.Value),
                new SqlParameter("@IsBenar", dto.IsBenar)
            );
            return Ok(new { message = "Pilihan jawaban berhasil ditambahkan" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var pj = await _context.PilihanJawabans.FindAsync(id);
        if (pj == null) return NotFound();

        _context.PilihanJawabans.Remove(pj);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}

public class CreatePilihanDto
{
    public int BankSoalId { get; set; }
    public string Label { get; set; } = null!;
    public string? TeksJawaban { get; set; }
    public string? GambarJawaban { get; set; }
    public bool IsBenar { get; set; }
}