using ApiSIS.Data;
using ApiSIS.Dtos;
using ApiSIS.DTOs;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace ApiSIS.Services;

public interface IBankSoalService
{
    Task<List<BankSoalDto>> GetByMapelAsync(BankSoalFilterDto filter);
    Task<BankSoalDetailDto?> GetDetailAsync(int id);
    Task<int> CreateAsync(BankSoalCreateDto dto);
    Task<bool> UpdateAsync(int id, BankSoalUpdateDto dto);
    Task<bool> DeleteAsync(int id, bool softDelete = true);
    Task<List<StatistikDto>> GetStatistikAsync();
}

public class BankSoalService : IBankSoalService
{
    private readonly AppDbContext _context;

    public BankSoalService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<BankSoalDto>> GetByMapelAsync(BankSoalFilterDto filter)
    {
        var query = _context.BankSoals
            .Include(x => x.MataPelajaran)
            .AsQueryable();

        if (filter.MataPelajaranId.HasValue)
            query = query.Where(x => x.MataPelajaranId == filter.MataPelajaranId.Value);

        if (!string.IsNullOrEmpty(filter.TipeSoal))
            query = query.Where(x => x.TipeSoal == filter.TipeSoal);

        if (!string.IsNullOrEmpty(filter.TingkatKesulitan))
            query = query.Where(x => x.TingkatKesulitan == filter.TingkatKesulitan);

        if (!string.IsNullOrEmpty(filter.Status))
            query = query.Where(x => x.Status == filter.Status);

        return await query
            .OrderBy(x => x.MataPelajaran.Nama).ThenBy(x => x.Id)
            .Select(x => new BankSoalDto
            {
                Id = x.Id,
                MataPelajaranId = x.MataPelajaranId,
                NamaMapel = x.MataPelajaran.Nama,
                TipeSoal = x.TipeSoal,
                Pertanyaan = x.Pertanyaan,
                GambarSoal = x.GambarSoal,
                TingkatKesulitan = x.TingkatKesulitan,
                BobotNilai = x.BobotNilai,
                Pembahasan = x.Pembahasan,
                Status = x.Status
            })
            .ToListAsync();
    }

    public async Task<BankSoalDetailDto?> GetDetailAsync(int id)
    {
        var soal = await _context.BankSoals
            .Include(x => x.MataPelajaran)
            .Include(x => x.PilihanJawabans)
            .Include(x => x.KunciEssay)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (soal == null) return null;

        return new BankSoalDetailDto
        {
            Id = soal.Id,
            MataPelajaranId = soal.MataPelajaranId,
            NamaMapel = soal.MataPelajaran.Nama,
            TipeSoal = soal.TipeSoal,
            Pertanyaan = soal.Pertanyaan,
            GambarSoal = soal.GambarSoal,
            TingkatKesulitan = soal.TingkatKesulitan,
            BobotNilai = soal.BobotNilai,
            Pembahasan = soal.Pembahasan,
            Status = soal.Status,
            PilihanJawaban = soal.PilihanJawabans
                .OrderBy(p => p.Label)
                .Select(p => new PilihanJawabanDto
                {
                    Id = p.Id,
                    Label = p.Label,
                    TeksJawaban = p.TeksJawaban,
                    GambarJawaban = p.GambarJawaban,
                    IsBenar = p.IsBenar
                }).ToList(),
            KunciEssay = soal.KunciEssay == null ? null : new KunciEssayDto
            {
                KunciJawaban = soal.KunciEssay.KunciJawaban,
                RubrikPenilaian = soal.KunciEssay.RubrikPenilaian
            }
        };
    }

    public async Task<int> CreateAsync(BankSoalCreateDto dto)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            // Panggil SP insert bank_soal
            var newIdParam = new SqlParameter("@NewId", System.Data.SqlDbType.Int)
            {
                Direction = System.Data.ParameterDirection.Output
            };

            await _context.Database.ExecuteSqlRawAsync(
                "EXEC sp_BankSoal_Insert @MataPelajaranId, @TipeSoal, @Pertanyaan, @GambarSoal, @TingkatKesulitan, @BobotNilai, @Pembahasan, @CreatedBy, @NewId OUTPUT",
                new SqlParameter("@MataPelajaranId", dto.MataPelajaranId),
                new SqlParameter("@TipeSoal", dto.TipeSoal),
                new SqlParameter("@Pertanyaan", dto.Pertanyaan),
                new SqlParameter("@GambarSoal", (object?)dto.GambarSoal ?? DBNull.Value),
                new SqlParameter("@TingkatKesulitan", dto.TingkatKesulitan),
                new SqlParameter("@BobotNilai", dto.BobotNilai),
                new SqlParameter("@Pembahasan", (object?)dto.Pembahasan ?? DBNull.Value),
                new SqlParameter("@CreatedBy", (object?)dto.CreatedBy ?? DBNull.Value),
                newIdParam
            );

            int newId = (int)newIdParam.Value;

            // Insert pilihan jawaban (kalau MC)
            if (dto.PilihanJawaban?.Any() == true)
            {
                foreach (var pj in dto.PilihanJawaban)
                {
                    await _context.Database.ExecuteSqlRawAsync(
                        "EXEC sp_PilihanJawaban_Insert @BankSoalId, @Label, @TeksJawaban, @GambarJawaban, @IsBenar",
                        new SqlParameter("@BankSoalId", newId),
                        new SqlParameter("@Label", pj.Label),
                        new SqlParameter("@TeksJawaban", (object?)pj.TeksJawaban ?? DBNull.Value),
                        new SqlParameter("@GambarJawaban", (object?)pj.GambarJawaban ?? DBNull.Value),
                        new SqlParameter("@IsBenar", pj.IsBenar)
                    );
                }
            }

            // Insert kunci essay (kalau essay)
            if (dto.KunciEssay != null)
            {
                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC sp_KunciEssay_Upsert @BankSoalId, @KunciJawaban, @RubrikPenilaian",
                    new SqlParameter("@BankSoalId", newId),
                    new SqlParameter("@KunciJawaban", (object?)dto.KunciEssay.KunciJawaban ?? DBNull.Value),
                    new SqlParameter("@RubrikPenilaian", (object?)dto.KunciEssay.RubrikPenilaian ?? DBNull.Value)
                );
            }

            await transaction.CommitAsync();
            return newId;
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task<bool> UpdateAsync(int id, BankSoalUpdateDto dto)
    {
        var soal = await _context.BankSoals.FindAsync(id);
        if (soal == null) return false;

        soal.MataPelajaranId = dto.MataPelajaranId ?? soal.MataPelajaranId;
        soal.Pertanyaan = dto.Pertanyaan ?? soal.Pertanyaan;
        soal.GambarSoal = dto.GambarSoal ?? soal.GambarSoal;
        soal.TingkatKesulitan = dto.TingkatKesulitan ?? soal.TingkatKesulitan;
        soal.BobotNilai = dto.BobotNilai ?? soal.BobotNilai;
        soal.Pembahasan = dto.Pembahasan ?? soal.Pembahasan;
        soal.Status = dto.Status ?? soal.Status;
        soal.UpdatedAt = DateTime.Now;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id, bool softDelete = true)
    {
        var soal = await _context.BankSoals.FindAsync(id);
        if (soal == null) return false;

        if (softDelete)
        {
            soal.Status = "nonaktif";
            soal.UpdatedAt = DateTime.Now;
        }
        else
        {
            _context.BankSoals.Remove(soal);
        }

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<List<StatistikDto>> GetStatistikAsync()
    {
        return await _context.Database
            .SqlQueryRaw<StatistikDto>("EXEC sp_BankSoal_Statistik")
            .ToListAsync();
    }
}

public class StatistikDto
{
    public string MataPelajaran { get; set; } = null!;
    public int TotalMc { get; set; }
    public int TotalEssay { get; set; }
    public int TotalSoal { get; set; }
    public int Mudah { get; set; }
    public int Sedang { get; set; }
    public int Sulit { get; set; }
}