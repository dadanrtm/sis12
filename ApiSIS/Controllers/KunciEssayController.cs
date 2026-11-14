using ApiSIS.Data;
using ApiSIS.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace ApiSIS.Controllers;

[ApiController]
[Route("api/[controller]")]
public class KunciEssayController : ControllerBase
{
    private readonly AppDbContext _context;

    public KunciEssayController(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>Ambil kunci essay berdasarkan ID Soal</summary>
    [HttpGet("by-soal/{bankSoalId}")]
    public async Task<IActionResult> GetBySoal(int bankSoalId)
    {
        var data = await _context.KunciEssays
            .Where(x => x.BankSoalId == bankSoalId)
            .Select(x => new KunciEssayDto
            {
                KunciJawaban = x.KunciJawaban,
                RubrikPenilaian = x.RubrikPenilaian
            })
            .FirstOrDefaultAsync();

        if (data == null) return NotFound(new { message = "Kunci essay tidak ditemukan" });
        return Ok(data);
    }

    /// <summary>Tambah atau Update kunci essay (Upsert)</summary>
    [HttpPost("upsert/{bankSoalId}")]
    public async Task<IActionResult> Upsert(int bankSoalId, [FromBody] KunciEssayCreateDto dto)
    {
        try
        {
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC sp_KunciEssay_Upsert @BankSoalId, @KunciJawaban, @RubrikPenilaian",
                new SqlParameter("@BankSoalId", bankSoalId),
                new SqlParameter("@KunciJawaban", (object?)dto.KunciJawaban ?? DBNull.Value),
                new SqlParameter("@RubrikPenilaian", (object?)dto.RubrikPenilaian ?? DBNull.Value)
            );
            return Ok(new { message = "Kunci essay berhasil disimpan" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    /// <summary>Hapus kunci essay</summary>
    [HttpDelete("{bankSoalId}")]
    public async Task<IActionResult> Delete(int bankSoalId)
    {
        var ke = await _context.KunciEssays.FirstOrDefaultAsync(x => x.BankSoalId == bankSoalId);
        if (ke == null) return NotFound(new { message = "Kunci essay tidak ditemukan" });

        _context.KunciEssays.Remove(ke);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}