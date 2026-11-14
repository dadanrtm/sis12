using System;
using System.Collections.Generic;
using ApiSIS.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApiSIS.Data;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AbsensiSiswa> AbsensiSiswas { get; set; }

    public virtual DbSet<BankSoal> BankSoals { get; set; }

    public virtual DbSet<IzinSiswa> IzinSiswas { get; set; }

    public virtual DbSet<JadwalPelajaran> JadwalPelajarans { get; set; }

    public virtual DbSet<JawabanSiswa> JawabanSiswas { get; set; }

    public virtual DbSet<KunciEssay> KunciEssays { get; set; }

    public virtual DbSet<LogAbsensi> LogAbsensis { get; set; }

    public virtual DbSet<MataPelajaran> MataPelajarans { get; set; }

    public virtual DbSet<Menu> Menus { get; set; }

    public virtual DbSet<MsClass> MsClasses { get; set; }

    public virtual DbSet<MsStudent> MsStudents { get; set; }

    public virtual DbSet<MsTeacher> MsTeachers { get; set; }

    public virtual DbSet<MsUser> MsUsers { get; set; }

    public virtual DbSet<PilihanJawaban> PilihanJawabans { get; set; }

    public virtual DbSet<RekapAbsensi> RekapAbsensis { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<RoleMenuMapping> RoleMenuMappings { get; set; }

    public virtual DbSet<SettingAbsensi> SettingAbsensis { get; set; }

    public virtual DbSet<Ujian> Ujians { get; set; }

    public virtual DbSet<UjianSoal> UjianSoals { get; set; }

    public virtual DbSet<UserRoleMapping> UserRoleMappings { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=ABSENSI;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AbsensiSiswa>(entity =>
        {
            entity.HasKey(e => e.IdAbsensi).HasName("PK__absensi___570F960D43CA461A");

            entity.ToTable("absensi_Siswa");

            entity.Property(e => e.IdAbsensi).HasColumnName("id_absensi");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.FotoDatang)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("foto_datang");
            entity.Property(e => e.FotoPulang)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("foto_pulang");
            entity.Property(e => e.IdStudent).HasColumnName("id_student");
            entity.Property(e => e.JamDatang).HasColumnName("jam_datang");
            entity.Property(e => e.JamPulang).HasColumnName("jam_pulang");
            entity.Property(e => e.Keterangan)
                .HasColumnType("text")
                .HasColumnName("keterangan");
            entity.Property(e => e.LatitudeDatang)
                .HasColumnType("decimal(10, 8)")
                .HasColumnName("latitude_datang");
            entity.Property(e => e.LatitudePulang)
                .HasColumnType("decimal(10, 8)")
                .HasColumnName("latitude_pulang");
            entity.Property(e => e.LokasiDatang)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("lokasi_datang");
            entity.Property(e => e.LokasiPulang)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("lokasi_pulang");
            entity.Property(e => e.LongitudeDatang)
                .HasColumnType("decimal(11, 8)")
                .HasColumnName("longitude_datang");
            entity.Property(e => e.LongitudePulang)
                .HasColumnType("decimal(11, 8)")
                .HasColumnName("longitude_pulang");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("alfa")
                .HasColumnName("status");
            entity.Property(e => e.Tanggal).HasColumnName("tanggal");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.AbsensiSiswas)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK__absensi_S__creat__66603565");

            entity.HasOne(d => d.IdStudentNavigation).WithMany(p => p.AbsensiSiswas)
                .HasForeignKey(d => d.IdStudent)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__absensi_S__id_st__6754599E");
        });

        modelBuilder.Entity<BankSoal>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__bank_soa__3213E83F0810B206");

            entity.ToTable("bank_soal");

            entity.HasIndex(e => e.MataPelajaranId, "idx_bs_mapel");

            entity.HasIndex(e => e.TipeSoal, "idx_bs_tipe");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BobotNilai)
                .HasDefaultValue(1)
                .HasColumnName("bobot_nilai");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.GambarSoal)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("gambar_soal");
            entity.Property(e => e.MataPelajaranId).HasColumnName("mata_pelajaran_id");
            entity.Property(e => e.Pembahasan).HasColumnName("pembahasan");
            entity.Property(e => e.Pertanyaan).HasColumnName("pertanyaan");
            entity.Property(e => e.Status)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasDefaultValue("aktif")
                .HasColumnName("status");
            entity.Property(e => e.TingkatKesulitan)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasDefaultValue("sedang")
                .HasColumnName("tingkat_kesulitan");
            entity.Property(e => e.TipeSoal)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("tipe_soal");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.MataPelajaran).WithMany(p => p.BankSoals)
                .HasForeignKey(d => d.MataPelajaranId)
                .HasConstraintName("fk_bs_mapel");
        });

        modelBuilder.Entity<IzinSiswa>(entity =>
        {
            entity.HasKey(e => e.IdIzin).HasName("PK__izin_sis__8574309435CD5278");

            entity.ToTable("izin_siswa");

            entity.Property(e => e.IdIzin).HasColumnName("id_izin");
            entity.Property(e => e.Alasan)
                .HasColumnType("text")
                .HasColumnName("alasan");
            entity.Property(e => e.ApprovedAt)
                .HasColumnType("datetime")
                .HasColumnName("approved_at");
            entity.Property(e => e.ApprovedBy).HasColumnName("approved_by");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.IdStudent).HasColumnName("id_student");
            entity.Property(e => e.JenisIzin)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("jenis_izin");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("pending")
                .HasColumnName("status");
            entity.Property(e => e.SuratKeterangan)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("surat_keterangan");
            entity.Property(e => e.TanggalAkhir).HasColumnName("tanggal_akhir");
            entity.Property(e => e.TanggalMulai).HasColumnName("tanggal_mulai");

            entity.HasOne(d => d.ApprovedByNavigation).WithMany(p => p.IzinSiswas)
                .HasForeignKey(d => d.ApprovedBy)
                .HasConstraintName("FK__izin_sisw__appro__68487DD7");

            entity.HasOne(d => d.IdStudentNavigation).WithMany(p => p.IzinSiswas)
                .HasForeignKey(d => d.IdStudent)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__izin_sisw__id_st__693CA210");
        });

        modelBuilder.Entity<JadwalPelajaran>(entity =>
        {
            entity.HasKey(e => e.IdJadwal).HasName("PK__jadwal_p__FBC3302E7E8C2A3B");

            entity.ToTable("jadwal_pelajaran");

            entity.Property(e => e.IdJadwal).HasColumnName("id_jadwal");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Hari)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("hari");
            entity.Property(e => e.IdClass).HasColumnName("id_class");
            entity.Property(e => e.IdTeacher).HasColumnName("id_teacher");
            entity.Property(e => e.JamMulai).HasColumnName("jam_mulai");
            entity.Property(e => e.JamSelesai).HasColumnName("jam_selesai");
            entity.Property(e => e.MataPelajaran)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("mata_pelajaran");
            entity.Property(e => e.Ruangan)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ruangan");

            entity.HasOne(d => d.IdClassNavigation).WithMany(p => p.JadwalPelajarans)
                .HasForeignKey(d => d.IdClass)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__jadwal_pe__id_cl__6A30C649");

            entity.HasOne(d => d.IdTeacherNavigation).WithMany(p => p.JadwalPelajarans)
                .HasForeignKey(d => d.IdTeacher)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__jadwal_pe__id_te__6B24EA82");
        });

        modelBuilder.Entity<JawabanSiswa>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__jawaban___3213E83FE4506AAF");

            entity.ToTable("jawaban_siswa");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BankSoalId).HasColumnName("bank_soal_id");
            entity.Property(e => e.GambarJawaban)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("gambar_jawaban");
            entity.Property(e => e.JawabanEssay).HasColumnName("jawaban_essay");
            entity.Property(e => e.Nilai)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("nilai");
            entity.Property(e => e.PilihanJawabanId).HasColumnName("pilihan_jawaban_id");
            entity.Property(e => e.SiswaId).HasColumnName("siswa_id");
            entity.Property(e => e.UjianId).HasColumnName("ujian_id");

            entity.HasOne(d => d.BankSoal).WithMany(p => p.JawabanSiswas)
                .HasForeignKey(d => d.BankSoalId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_js_soal");

            entity.HasOne(d => d.PilihanJawaban).WithMany(p => p.JawabanSiswas)
                .HasForeignKey(d => d.PilihanJawabanId)
                .HasConstraintName("fk_js_pil");

            entity.HasOne(d => d.Ujian).WithMany(p => p.JawabanSiswas)
                .HasForeignKey(d => d.UjianId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_js_ujian");
        });

        modelBuilder.Entity<KunciEssay>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__kunci_es__3213E83F263BABF8");

            entity.ToTable("kunci_essay");

            entity.HasIndex(e => e.BankSoalId, "UQ__kunci_es__AE9FD26B177001BE").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BankSoalId).HasColumnName("bank_soal_id");
            entity.Property(e => e.KunciJawaban).HasColumnName("kunci_jawaban");
            entity.Property(e => e.RubrikPenilaian).HasColumnName("rubrik_penilaian");

            entity.HasOne(d => d.BankSoal).WithOne(p => p.KunciEssay)
                .HasForeignKey<KunciEssay>(d => d.BankSoalId)
                .HasConstraintName("fk_ke_soal");
        });

        modelBuilder.Entity<LogAbsensi>(entity =>
        {
            entity.HasKey(e => e.IdLog).HasName("PK__log_abse__6CC851FE37EEEF09");

            entity.ToTable("log_absensi");

            entity.Property(e => e.IdLog).HasColumnName("id_log");
            entity.Property(e => e.Action)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("action");
            entity.Property(e => e.ChangedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("changed_at");
            entity.Property(e => e.ChangedBy).HasColumnName("changed_by");
            entity.Property(e => e.IdAbsensi).HasColumnName("id_absensi");
            entity.Property(e => e.NewData)
                .HasColumnType("text")
                .HasColumnName("new_data");
            entity.Property(e => e.OldData)
                .HasColumnType("text")
                .HasColumnName("old_data");

            entity.HasOne(d => d.ChangedByNavigation).WithMany(p => p.LogAbsensis)
                .HasForeignKey(d => d.ChangedBy)
                .HasConstraintName("FK__log_absen__chang__6C190EBB");
        });

        modelBuilder.Entity<MataPelajaran>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__mata_pel__3213E83FF0B8B36B");

            entity.ToTable("mata_pelajaran");

            entity.HasIndex(e => e.Kode, "UQ__mata_pel__25A87492B50E57D9").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Deskripsi).HasColumnName("deskripsi");
            entity.Property(e => e.Kode)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("kode");
            entity.Property(e => e.Nama)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nama");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<Menu>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__menu__3213E83FBA749513");

            entity.ToTable("menu");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Component)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("component");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Icon)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("icon");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.IsCache)
                .HasDefaultValue(false)
                .HasColumnName("is_cache");
            entity.Property(e => e.IsFrame)
                .HasDefaultValue(false)
                .HasColumnName("is_frame");
            entity.Property(e => e.MenuName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("menu_name");
            entity.Property(e => e.MenuTitle)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("menu_title");
            entity.Property(e => e.MenuType)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValue("C")
                .IsFixedLength()
                .HasColumnName("menu_type");
            entity.Property(e => e.OrderNum).HasColumnName("order_num");
            entity.Property(e => e.ParentId).HasColumnName("parent_id");
            entity.Property(e => e.Path)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("path");
            entity.Property(e => e.Perms)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("perms");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.Visible).HasColumnName("visible");
        });

        modelBuilder.Entity<MsClass>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__msClass__3213E83F103C267D");

            entity.ToTable("msClass");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ClassName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("class_name");
            entity.Property(e => e.GradeLevel).HasColumnName("grade_level");
            entity.Property(e => e.HomeroomTeacherId).HasColumnName("homeroom_teacher_id");
        });

        modelBuilder.Entity<MsStudent>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__msStuden__3213E83F65278C5D");

            entity.ToTable("msStudent");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("address");
            entity.Property(e => e.BirthDate).HasColumnName("birth_date");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_date");
            entity.Property(e => e.Gender)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("gender");
            entity.Property(e => e.Nis)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nis");
            entity.Property(e => e.Nisn)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nisn");
            entity.Property(e => e.ParentName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("parent_name");
            entity.Property(e => e.ParentPhone)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("parent_phone");
            entity.Property(e => e.Status)
                .HasDefaultValue(true)
                .HasColumnName("status");
            entity.Property(e => e.StudentName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("student_name");
        });

        modelBuilder.Entity<MsTeacher>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__msTeache__3213E83F763D3484");

            entity.ToTable("msTeacher");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Nip)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nip");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("phone");
            entity.Property(e => e.Status)
                .HasDefaultValue(true)
                .HasColumnName("status");
            entity.Property(e => e.TeacherName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("teacher_name");
        });

        modelBuilder.Entity<MsUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__msUser__3213E83FBD86AB48");

            entity.ToTable("msUser");

            entity.HasIndex(e => e.Username, "UQ__msUser__F3DBC57227C382E3").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.FullName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("full_name");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("phone");
            entity.Property(e => e.Status)
                .HasDefaultValue(true)
                .HasColumnName("status");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("username");
        });

        modelBuilder.Entity<PilihanJawaban>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__pilihan___3213E83FE0084461");

            entity.ToTable("pilihan_jawaban");

            entity.HasIndex(e => e.BankSoalId, "idx_pj_soal");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BankSoalId).HasColumnName("bank_soal_id");
            entity.Property(e => e.GambarJawaban)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("gambar_jawaban");
            entity.Property(e => e.IsBenar)
                .HasDefaultValue(false)
                .HasColumnName("is_benar");
            entity.Property(e => e.Label)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("label");
            entity.Property(e => e.TeksJawaban).HasColumnName("teks_jawaban");

            entity.HasOne(d => d.BankSoal).WithMany(p => p.PilihanJawabans)
                .HasForeignKey(d => d.BankSoalId)
                .HasConstraintName("fk_pj_soal");
        });

        modelBuilder.Entity<RekapAbsensi>(entity =>
        {
            entity.HasKey(e => e.IdRekap).HasName("PK__rekap_ab__43E18801D8AE7E92");

            entity.ToTable("rekap_absensi");

            entity.HasIndex(e => new { e.IdStudent, e.Bulan, e.Tahun }, "UQ_rekap").IsUnique();

            entity.Property(e => e.IdRekap).HasColumnName("id_rekap");
            entity.Property(e => e.Alfa)
                .HasDefaultValue(0)
                .HasColumnName("alfa");
            entity.Property(e => e.Bulan).HasColumnName("bulan");
            entity.Property(e => e.Hadir)
                .HasDefaultValue(0)
                .HasColumnName("hadir");
            entity.Property(e => e.IdStudent).HasColumnName("id_student");
            entity.Property(e => e.Izin)
                .HasDefaultValue(0)
                .HasColumnName("izin");
            entity.Property(e => e.PersentaseKehadiran)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("persentase_kehadiran");
            entity.Property(e => e.Sakit)
                .HasDefaultValue(0)
                .HasColumnName("sakit");
            entity.Property(e => e.Tahun).HasColumnName("tahun");
            entity.Property(e => e.Terlambat)
                .HasDefaultValue(0)
                .HasColumnName("terlambat");
            entity.Property(e => e.TotalHariEfektif)
                .HasDefaultValue(0)
                .HasColumnName("total_hari_efektif");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.IdStudentNavigation).WithMany(p => p.RekapAbsensis)
                .HasForeignKey(d => d.IdStudent)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__rekap_abs__id_st__6D0D32F4");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__role__3213E83F77FE8209");

            entity.ToTable("role");

            entity.HasIndex(e => e.RoleCode, "UQ__role__BAE6307564CC402F").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.RoleCode)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("role_code");
            entity.Property(e => e.RoleName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("role_name");
            entity.Property(e => e.Status)
                .HasDefaultValue(true)
                .HasColumnName("status");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<RoleMenuMapping>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__role_men__3213E83F335C529E");

            entity.ToTable("role_menu_mapping");

            entity.HasIndex(e => new { e.RoleId, e.MenuId }, "UQ_role_menu").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.MenuId).HasColumnName("menu_id");
            entity.Property(e => e.RoleId).HasColumnName("role_id");

            entity.HasOne(d => d.Menu).WithMany(p => p.RoleMenuMappings)
                .HasForeignKey(d => d.MenuId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_role_menu_menu");

            entity.HasOne(d => d.Role).WithMany(p => p.RoleMenuMappings)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_role_menu_role");
        });

        modelBuilder.Entity<SettingAbsensi>(entity =>
        {
            entity.HasKey(e => e.IdSetting).HasName("PK__setting___6C27915A816E56BD");

            entity.ToTable("setting_absensi");

            entity.HasIndex(e => e.SettingKey, "UQ__setting___0DFAC4275978EBE9").IsUnique();

            entity.Property(e => e.IdSetting).HasColumnName("id_setting");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Deskripsi)
                .HasColumnType("text")
                .HasColumnName("deskripsi");
            entity.Property(e => e.SettingKey)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("setting_key");
            entity.Property(e => e.SettingValue)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("setting_value");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<Ujian>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ujian__3213E83F2420E4CE");

            entity.ToTable("ujian");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DurasiMenit)
                .HasDefaultValue(60)
                .HasColumnName("durasi_menit");
            entity.Property(e => e.MataPelajaranId).HasColumnName("mata_pelajaran_id");
            entity.Property(e => e.NamaUjian)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("nama_ujian");
            entity.Property(e => e.Status)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasDefaultValue("draft")
                .HasColumnName("status");
            entity.Property(e => e.TanggalMulai)
                .HasColumnType("datetime")
                .HasColumnName("tanggal_mulai");
            entity.Property(e => e.TanggalSelesai)
                .HasColumnType("datetime")
                .HasColumnName("tanggal_selesai");

            entity.HasOne(d => d.MataPelajaran).WithMany(p => p.Ujians)
                .HasForeignKey(d => d.MataPelajaranId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_ujian_mapel");
        });

        modelBuilder.Entity<UjianSoal>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ujian_so__3213E83F52E63D40");

            entity.ToTable("ujian_soal");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BankSoalId).HasColumnName("bank_soal_id");
            entity.Property(e => e.UjianId).HasColumnName("ujian_id");
            entity.Property(e => e.Urutan)
                .HasDefaultValue(1)
                .HasColumnName("urutan");

            entity.HasOne(d => d.BankSoal).WithMany(p => p.UjianSoals)
                .HasForeignKey(d => d.BankSoalId)
                .HasConstraintName("fk_us_soal");

            entity.HasOne(d => d.Ujian).WithMany(p => p.UjianSoals)
                .HasForeignKey(d => d.UjianId)
                .HasConstraintName("fk_us_ujian");
        });

        modelBuilder.Entity<UserRoleMapping>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__user_rol__3213E83F523FF47E");

            entity.ToTable("user_role_mapping");

            entity.HasIndex(e => new { e.UserId, e.RoleId }, "UQ_user_role").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Role).WithMany(p => p.UserRoleMappings)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_user_role_role");

            entity.HasOne(d => d.User).WithMany(p => p.UserRoleMappings)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_user_role_user");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
