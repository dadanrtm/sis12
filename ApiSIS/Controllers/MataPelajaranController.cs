using ApiSIS.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiSIS.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MataPelajaranController : ControllerBase
{
    private readonly AppDbContext _context;

    public MataPelajaranController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var data = await _context.MataPelajarans
            .Select(x => new { x.Id, x.Kode, x.Nama, x.Deskripsi })
            .ToListAsync();
        return Ok(data);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var data = await _context.MataPelajarans.FindAsync(id);
        if (data == null) return NotFound(new { message = "Mata pelajaran tidak ditemukan" });
        return Ok(data);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateMapelDto dto)
    {
        var mapel = new ApiSIS.Entities.MataPelajaran
        {
            Kode = dto.Kode,
            Nama = dto.Nama,
            Deskripsi = dto.Deskripsi,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };
        _context.MataPelajarans.Add(mapel);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = mapel.Id }, mapel);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] CreateMapelDto dto)
    {
        var mapel = await _context.MataPelajarans.FindAsync(id);
        if (mapel == null) return NotFound();

        mapel.Kode = dto.Kode;
        mapel.Nama = dto.Nama;
        mapel.Deskripsi = dto.Deskripsi;
        mapel.UpdatedAt = DateTime.Now;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var mapel = await _context.MataPelajarans.FindAsync(id);
        if (mapel == null) return NotFound();

        _context.MataPelajarans.Remove(mapel);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}

public class CreateMapelDto
{
    public string Kode { get; set; } = null!;
    public string Nama { get; set; } = null!;
    public string? Deskripsi { get; set; }
}