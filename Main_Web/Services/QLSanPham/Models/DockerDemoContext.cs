using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace QLSanPham.Models
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
        
        public virtual DbSet<SanPham> SanPham { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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
