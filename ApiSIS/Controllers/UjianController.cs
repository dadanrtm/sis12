using ApiSIS.Dtos;
using ApiSIS.DTOs;
using ApiSIS.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiSIS.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UjianController : ControllerBase
{
    private readonly IUjianService _service;

    public UjianController(IUjianService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _service.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var data = await _service.GetByIdAsync(id);
        if (data == null) return NotFound();
        return Ok(data);
    }

    /// <summary>Generate soal acak untuk ujian</summary>
    [HttpPost("{id}/generate-soal")]
    public async Task<IActionResult> GenerateSoal(int id, [FromBody] GenerateSoalDto dto)
    {
        try
        {
            var count = await _service.GenerateSoalAsync(id, dto);
            return Ok(new { message = $"Berhasil generate {count} soal", jumlahSoal = count });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    /// <summary>Submit jawaban siswa (MC otomatis dinilai, essay manual)</summary>
    [HttpPost("submit-jawaban")]
    public async Task<IActionResult> SubmitJawaban([FromBody] SubmitJawabanDto dto)
    {
        try
        {
            await _service.SubmitJawabanAsync(dto);
            return Ok(new { message = "Jawaban berhasil disimpan" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    /// <summary>Hitung nilai akhir siswa berdasarkan ujian</summary>
    [HttpGet("{ujianId}/nilai/{siswaId}")]
    public async Task<IActionResult> HitungNilai(int ujianId, int siswaId)
    {
        var data = await _service.HitungNilaiAsync(ujianId, siswaId);
        if (data == null) return NotFound();
        return Ok(data);
    }
}