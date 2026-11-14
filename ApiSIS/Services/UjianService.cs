using ApiSIS.Data;
using ApiSIS.Dtos;
using ApiSIS.DTOs;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace ApiSIS.Services;

// 👇 INI INTERFACE YANG DICARI OLEH CONTROLLER
public interface IUjianService
{
    Task<List<UjianDto>> GetAllAsync();
    Task<UjianDto?> GetByIdAsync(int id);
    Task<int> GenerateSoalAsync(int ujianId, GenerateSoalDto dto);
    Task<bool> SubmitJawabanAsync(SubmitJawabanDto dto);
    Task<NilaiAkhirDto?> HitungNilaiAsync(int ujianId, int siswaId);
}

public class UjianService : IUjianService
{
    private readonly AppDbContext _context;

    public UjianService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<UjianDto>> GetAllAsync()
    {
        return await _context.Ujians
            .Include(x => x.MataPelajaran)
            .Include(x => x.UjianSoals)
            .Select(x => new UjianDto
            {
                Id = x.Id,
                NamaUjian = x.NamaUjian,
                MataPelajaranId = x.MataPelajaranId,
                NamaMapel = x.MataPelajaran.Nama,
                DurasiMenit = x.DurasiMenit ?? 60,
                TanggalMulai = x.TanggalMulai,
                TanggalSelesai = x.TanggalSelesai,
                Status = x.Status,
                JumlahSoal = x.UjianSoals.Count
            })
            .ToListAsync();
    }

    public async Task<UjianDto?> GetByIdAsync(int id)
    {
        return await _context.Ujians
            .Include(x => x.MataPelajaran)
            .Include(x => x.UjianSoals)
            .Where(x => x.Id == id)
            .Select(x => new UjianDto
            {
                Id = x.Id,
                NamaUjian = x.NamaUjian,
                MataPelajaranId = x.MataPelajaranId,
                NamaMapel = x.MataPelajaran.Nama,
                DurasiMenit = x.DurasiMenit ?? 60,
                TanggalMulai = x.TanggalMulai,
                TanggalSelesai = x.TanggalSelesai,
                Status = x.Status,
                JumlahSoal = x.UjianSoals.Count
            })
            .FirstOrDefaultAsync();
    }

    public async Task<int> GenerateSoalAsync(int ujianId, GenerateSoalDto dto)
    {
        var result = await _context.Database
            .SqlQueryRaw<SoalUjianDto>(
                "EXEC sp_Ujian_GenerateSoalRandom @UjianId, @JumlahMC, @JumlahEssay, @MataPelajaranId",
                new SqlParameter("@UjianId", ujianId),
                new SqlParameter("@JumlahMC", dto.JumlahMc),
                new SqlParameter("@JumlahEssay", dto.JumlahEssay),
                new SqlParameter("@MataPelajaranId", (object?)dto.MataPelajaranId ?? DBNull.Value)
            )
            .ToListAsync();

        return result.Count;
    }

    public async Task<bool> SubmitJawabanAsync(SubmitJawabanDto dto)
    {
        await _context.Database.ExecuteSqlRawAsync(
            "EXEC sp_JawabanSiswa_Submit @UjianId, @BankSoalId, @SiswaId, @PilihanJawabanId, @JawabanEssay, @GambarJawaban",
            new SqlParameter("@UjianId", dto.UjianId),
            new SqlParameter("@BankSoalId", dto.BankSoalId),
            new SqlParameter("@SiswaId", dto.SiswaId),
            new SqlParameter("@PilihanJawabanId", (object?)dto.PilihanJawabanId ?? DBNull.Value),
            new SqlParameter("@JawabanEssay", (object?)dto.JawabanEssay ?? DBNull.Value),
            new SqlParameter("@GambarJawaban", (object?)dto.GambarJawaban ?? DBNull.Value)
        );
        return true;
    }

    public async Task<NilaiAkhirDto?> HitungNilaiAsync(int ujianId, int siswaId)
    {
        var result = await _context.Database
            .SqlQueryRaw<NilaiAkhirDto>(
                "EXEC sp_Ujian_HitungNilai @UjianId, @SiswaId",
                new SqlParameter("@UjianId", ujianId),
                new SqlParameter("@SiswaId", siswaId)
            )
            .ToListAsync();

        return result.FirstOrDefault();
    }
}

// DTO khusus untuk menampung hasil SP Generate Soal
public class SoalUjianDto
{
    public int Urutan { get; set; }
    public int SoalId { get; set; }
    public string TipeSoal { get; set; } = null!;
    public string Pertanyaan { get; set; } = null!;
    public string? GambarSoal { get; set; }
    public int BobotNilai { get; set; }
}