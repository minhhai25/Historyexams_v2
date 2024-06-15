using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Historyexams.Models;

public partial class HistoryexamsContext : DbContext
{
    public HistoryexamsContext()
    {
    }

    public HistoryexamsContext(DbContextOptions<HistoryexamsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Baitest> Baitests { get; set; }

    public virtual DbSet<Cauhoi> Cauhois { get; set; }

    public virtual DbSet<Chuong> Chuongs { get; set; }

    public virtual DbSet<Dethi> Dethis { get; set; }

    public virtual DbSet<Dtch> Dtches { get; set; }

    public virtual DbSet<Mucdo> Mucdos { get; set; }

    public virtual DbSet<Taikhoan> Taikhoans { get; set; }

    public virtual DbSet<Tkdt> Tkdts { get; set; }

    public virtual DbSet<Traloi> Tralois { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=DESKTOP-OOQJGOT\\SQLEXPRESS;Database=Historyexams;Trusted_Connection=True;MultipleActiveResultSets=True; TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Baitest>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__BAITEST__3214EC27F7CCF8D2");

            entity.ToTable("BAITEST");

            entity.HasIndex(e => e.Mabaitest, "UQ__BAITEST__ED3040C4FCE799ED").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Iduser).HasColumnName("IDUSER");
            entity.Property(e => e.Isactive).HasColumnName("ISACTIVE");
            entity.Property(e => e.Isdelete).HasColumnName("ISDELETE");
            entity.Property(e => e.Mabaitest)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("MABAITEST");
            entity.Property(e => e.Mota)
                .HasColumnType("ntext")
                .HasColumnName("MOTA");
            entity.Property(e => e.Ngaytao)
                .HasColumnType("datetime")
                .HasColumnName("NGAYTAO");
            entity.Property(e => e.Nguoitao)
                .HasMaxLength(50)
                .HasColumnName("NGUOITAO");
            entity.Property(e => e.Tenbaitest)
                .HasMaxLength(100)
                .HasColumnName("TENBAITEST");
        });

        modelBuilder.Entity<Cauhoi>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CAUHOI__3214EC270BA3B6B7");

            entity.ToTable("CAUHOI");

            entity.HasIndex(e => e.Macauhoi, "UQ__CAUHOI__DCF1CAB68CAD02C0").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Idchuong).HasColumnName("IDCHUONG");
            entity.Property(e => e.Idmucdo).HasColumnName("IDMUCDO");
            entity.Property(e => e.Isactive).HasColumnName("ISACTIVE");
            entity.Property(e => e.Isdelete).HasColumnName("ISDELETE");
            entity.Property(e => e.Macauhoi)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("MACAUHOI");
            entity.Property(e => e.Ngaytao)
                .HasColumnType("datetime")
                .HasColumnName("NGAYTAO");
            entity.Property(e => e.Nguoitao)
                .HasMaxLength(50)
                .HasColumnName("NGUOITAO");
            entity.Property(e => e.Noidung)
                .HasMaxLength(200)
                .HasColumnName("NOIDUNG");
            entity.Property(e => e.PaA)
                .HasMaxLength(200)
                .HasColumnName("PA_A");
            entity.Property(e => e.PaB)
                .HasMaxLength(200)
                .HasColumnName("PA_B");
            entity.Property(e => e.PaC)
                .HasMaxLength(200)
                .HasColumnName("PA_C");
            entity.Property(e => e.PaD)
                .HasMaxLength(200)
                .HasColumnName("PA_D");
            entity.Property(e => e.PaDung)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("PA_DUNG");

            entity.HasOne(d => d.IdchuongNavigation).WithMany(p => p.Cauhois)
                .HasForeignKey(d => d.Idchuong)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CAUHOI__IDCHUONG__4222D4EF");

            entity.HasOne(d => d.IdmucdoNavigation).WithMany(p => p.Cauhois)
                .HasForeignKey(d => d.Idmucdo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CAUHOI__MAMUCDO__412EB0B6");
        });

        modelBuilder.Entity<Chuong>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CHUONG__3214EC27FAE83CD2");

            entity.ToTable("CHUONG");

            entity.HasIndex(e => e.Machuong, "UQ__CHUONG__7B73E9847A2B0AD0").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Isactive).HasColumnName("ISACTIVE");
            entity.Property(e => e.Isdelete).HasColumnName("ISDELETE");
            entity.Property(e => e.Machuong)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("MACHUONG");
            entity.Property(e => e.Mota)
                .HasColumnType("ntext")
                .HasColumnName("MOTA");
            entity.Property(e => e.Ngaytao)
                .HasColumnType("datetime")
                .HasColumnName("NGAYTAO");
            entity.Property(e => e.Nguoitao)
                .HasMaxLength(50)
                .HasColumnName("NGUOITAO");
            entity.Property(e => e.Tenchuong)
                .HasMaxLength(100)
                .HasColumnName("TENCHUONG");
        });

        modelBuilder.Entity<Dethi>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__DETHI__3214EC278F5E3020");

            entity.ToTable("DETHI");

            entity.HasIndex(e => e.Madethi, "UQ__DETHI__01E3D5678FCFBEF2").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Idbaitest).HasColumnName("IDBAITEST");
            entity.Property(e => e.Idchuong).HasColumnName("IDCHUONG");
            entity.Property(e => e.Idmucdo).HasColumnName("IDMUCDO");
            entity.Property(e => e.Isactive).HasColumnName("ISACTIVE");
            entity.Property(e => e.Isdelete).HasColumnName("ISDELETE");
            entity.Property(e => e.Madethi)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("MADETHI");
            entity.Property(e => e.Mota)
                .HasColumnType("ntext")
                .HasColumnName("MOTA");
            entity.Property(e => e.Ngaytao)
                .HasColumnType("datetime")
                .HasColumnName("NGAYTAO");
            entity.Property(e => e.Nguoitao)
                .HasMaxLength(50)
                .HasColumnName("NGUOITAO");
            entity.Property(e => e.Socauhoi).HasColumnName("SOCAUHOI");
            entity.Property(e => e.Tendethi)
                .HasMaxLength(100)
                .HasColumnName("TENDETHI");
            entity.Property(e => e.Thoigian).HasColumnName("THOIGIAN");

            entity.HasOne(d => d.IdbaitestNavigation).WithMany(p => p.Dethis)
                .HasForeignKey(d => d.Idbaitest)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DETHI_BAITEST");

            entity.HasOne(d => d.IdchuongNavigation).WithMany(p => p.Dethis)
                .HasForeignKey(d => d.Idchuong)
                .HasConstraintName("FK_DETHI_CHUONG");

            entity.HasOne(d => d.IdmucdoNavigation).WithMany(p => p.Dethis)
                .HasForeignKey(d => d.Idmucdo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DETHI_MUCDO");
        });

        modelBuilder.Entity<Dtch>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__DTCH__3214EC27EFE83F8D");

            entity.ToTable("DTCH");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Idcauhoi).HasColumnName("IDCAUHOI");
            entity.Property(e => e.Iddethi).HasColumnName("IDDETHI");

            entity.HasOne(d => d.IdcauhoiNavigation).WithMany(p => p.Dtches)
                .HasForeignKey(d => d.Idcauhoi)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DTCH__IDCAUHOI__4BAC3F29");

            entity.HasOne(d => d.IddethiNavigation).WithMany(p => p.Dtches)
                .HasForeignKey(d => d.Iddethi)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DTCH__IDDETHI__4AB81AF0");
        });

        modelBuilder.Entity<Mucdo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__MUCDO__3214EC275DAE0ECE");

            entity.ToTable("MUCDO");

            entity.HasIndex(e => e.Mamd, "UQ__MUCDO__603F69E690F5342B").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Isactive).HasColumnName("ISACTIVE");
            entity.Property(e => e.Isdelete).HasColumnName("ISDELETE");
            entity.Property(e => e.Mamd)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("MAMD");
            entity.Property(e => e.Mota)
                .HasColumnType("ntext")
                .HasColumnName("MOTA");
            entity.Property(e => e.Ngaytao)
                .HasColumnType("datetime")
                .HasColumnName("NGAYTAO");
            entity.Property(e => e.Nguoitao)
                .HasMaxLength(50)
                .HasColumnName("NGUOITAO");
            entity.Property(e => e.Tenmd)
                .HasMaxLength(100)
                .HasColumnName("TENMD");
        });

        modelBuilder.Entity<Taikhoan>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TAIKHOAN__3214EC2729AE3101");

            entity.ToTable("TAIKHOAN");

            entity.HasIndex(e => e.Email, "UQ__TAIKHOAN__161CF724AD631E0A").IsUnique();

            entity.HasIndex(e => e.Mataikhoan, "UQ__TAIKHOAN__2ED8B5160F99C081").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Diachi)
                .HasMaxLength(100)
                .HasColumnName("DIACHI");
            entity.Property(e => e.Dienthoai)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("DIENTHOAI");
            entity.Property(e => e.Email)
                .HasMaxLength(150)
                .HasColumnName("EMAIL");
            entity.Property(e => e.Gioitinh)
                .HasMaxLength(5)
                .HasColumnName("GIOITINH");
            entity.Property(e => e.Hoten)
                .HasMaxLength(100)
                .HasColumnName("HOTEN");
            entity.Property(e => e.Isactive).HasColumnName("ISACTIVE");
            entity.Property(e => e.Isdelete).HasColumnName("ISDELETE");
            entity.Property(e => e.Mataikhoan)
                .HasMaxLength(12)
                .IsUnicode(false)
                .HasColumnName("MATAIKHOAN");
            entity.Property(e => e.Matkhau)
                .HasMaxLength(255)
                .HasColumnName("MATKHAU");
            entity.Property(e => e.Ngaysua)
                .HasColumnType("datetime")
                .HasColumnName("NGAYSUA");
            entity.Property(e => e.Ngaytao)
                .HasColumnType("datetime")
                .HasColumnName("NGAYTAO");
            entity.Property(e => e.Nguoitao)
                .HasMaxLength(50)
                .HasColumnName("NGUOITAO");
        });

        modelBuilder.Entity<Tkdt>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TKDT__3214EC27508F27DE");

            entity.ToTable("TKDT");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Iddethi).HasColumnName("IDDETHI");
            entity.Property(e => e.Idtaikhoan).HasColumnName("IDTAIKHOAN");
            entity.Property(e => e.Lan).HasColumnName("LAN");
            entity.Property(e => e.Ngaythi)
                .HasColumnType("datetime")
                .HasColumnName("NGAYTHI");
            entity.Property(e => e.Tyle)
                .HasMaxLength(8)
                .HasColumnName("TYLE");

            entity.HasOne(d => d.IddethiNavigation).WithMany(p => p.Tkdts)
                .HasForeignKey(d => d.Iddethi)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TKDT__IDDETHI__5629CD9C");

            entity.HasOne(d => d.IdtaikhoanNavigation).WithMany(p => p.Tkdts)
                .HasForeignKey(d => d.Idtaikhoan)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TKDT__IDTAIKHOAN__5535A963");
        });

        modelBuilder.Entity<Traloi>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TRALOI__3214EC279E179F26");

            entity.ToTable("TRALOI");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Iddtch).HasColumnName("IDDTCH");
            entity.Property(e => e.Lan).HasColumnName("LAN");
            entity.Property(e => e.Ngaythi)
                .HasColumnType("datetime")
                .HasColumnName("NGAYTHI");
            entity.Property(e => e.PaChon)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("PA_CHON");
            entity.Property(e => e.PaDung)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("PA_DUNG");
            entity.Property(e => e.Tkdtid).HasColumnName("TKDTID");

            entity.HasOne(d => d.IddtchNavigation).WithMany(p => p.Tralois)
                .HasForeignKey(d => d.Iddtch)
                .HasConstraintName("FK__TRALOI__IDDTCH__4E88ABD4");

            entity.HasOne(d => d.Tkdt).WithMany(p => p.Tralois)
                .HasForeignKey(d => d.Tkdtid)
                .HasConstraintName("FK__TRALOI__TKDTID__68487DD7");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
