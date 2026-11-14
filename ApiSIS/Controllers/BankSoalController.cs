using ApiSIS.Dtos;
using ApiSIS.DTOs;
using ApiSIS.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiSIS.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BankSoalController : ControllerBase
{
    private readonly IBankSoalService _service;

    public BankSoalController(IBankSoalService service)
    {
        _service = service;
    }

    /// <summary>Ambil daftar soal dengan filter (mapel, tipe, kesulitan)</summary>
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] BankSoalFilterDto filter)
    {
        var data = await _service.GetByMapelAsync(filter);
        return Ok(data);
    }

    /// <summary>Ambil detail 1 soal (termasuk pilihan jawaban & kunci essay)</summary>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var data = await _service.GetDetailAsync(id);
        if (data == null) return NotFound(new { message = "Soal tidak ditemukan" });
        return Ok(data);
    }

    /// <summary>Tambah soal baru (MC atau Essay) sekaligus pilihan jawabannya</summary>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] BankSoalCreateDto dto)
    {
        try
        {
            var id = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id }, new { id, message = "Soal berhasil dibuat" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    /// <summary>Update data soal (partial update)</summary>
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] BankSoalUpdateDto dto)
    {
        var ok = await _service.UpdateAsync(id, dto);
        if (!ok) return NotFound(new { message = "Soal tidak ditemukan" });
        return NoContent();
    }

    /// <summary>Hapus soal (default: soft delete / nonaktif)</summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id, [FromQuery] bool softDelete = true)
    {
        var ok = await _service.DeleteAsync(id, softDelete);
        if (!ok) return NotFound(new { message = "Soal tidak ditemukan" });
        return NoContent();
    }

    /// <summary>Statistik jumlah soal per mata pelajaran</summary>
    [HttpGet("statistik")]
    public async Task<IActionResult> Statistik()
    {
        var data = await _service.GetStatistikAsync();
        return Ok(data);
    }

    /// <summary>Upload gambar soal (multipart/form-data)</summary>
    [HttpPost("upload-gambar")]
    public async Task<IActionResult> UploadGambar(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest(new { message = "File tidak boleh kosong" });

        var allowed = new[] { ".jpg", ".jpeg", ".png", ".gif" };
        var ext = Path.GetExtension(file.FileName).ToLower();
        if (!allowed.Contains(ext))
            return BadRequest(new { message = "Format file tidak didukung (jpg/png/gif)" });

        // Simpan ke folder wwwroot/uploads/soal
        var folder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "soal");
        Directory.CreateDirectory(folder);

        var fileName = $"{Guid.NewGuid()}{ext}";
        var path = Path.Combine(folder, fileName);

        using (var stream = new FileStream(path, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        var url = $"/uploads/soal/{fileName}";
        return Ok(new { url, message = "Upload berhasil" });
    }
}