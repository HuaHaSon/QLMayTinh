using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Seeder.Models
{
    public partial class DockerDemoContext : DbContext
    {
        public DockerDemoContext()
        {
        }

        public DockerDemoContext(DbContextOptions<DockerDemoContext> options)
            : base(options)
        {
        }

        public virtual DbSet<KhachHang> KhachHang { get; set; }
        public virtual DbSet<SanPham> SanPham { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<KhachHang>(entity =>
            {
                entity.HasKey(e => e.MaKh);

                entity.Property(e => e.MaKh)
                    .HasColumnName("MaKH")
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.DiaChi).HasMaxLength(100);

                entity.Property(e => e.GioiTinh).HasMaxLength(5);

                entity.Property(e => e.NgaySinh).HasColumnType("date");

                entity.Property(e => e.Sdt)
                    .HasColumnName("SDT")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.TenKh)
                    .HasColumnName("TenKH")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<SanPham>(entity =>
            {
                entity.HasKey(e => e.MaSp);

                entity.Property(e => e.MaSp)
                    .HasColumnName("MaSP")
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.DonGia).HasColumnType("decimal(19, 3)");

                entity.Property(e => e.Dvt)
                    .HasColumnName("DVT")
                    .HasMaxLength(20);

                entity.Property(e => e.Slton).HasColumnName("SLTon");

                entity.Property(e => e.TenSp)
                    .IsRequired()
                    .HasColumnName("TenSP")
                    .HasMaxLength(50);
            });
        }
    }
}
