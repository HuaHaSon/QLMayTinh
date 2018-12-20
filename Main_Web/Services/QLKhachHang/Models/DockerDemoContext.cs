using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace QLKhachHang.Models
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

            /*modelBuilder.Entity<KhachHang>().HasData(new KhachHang()
            {
                CustomerId = "KH001",
                CustomerName = "Trần Văn Bé",
                Gender = "Nam",
                Birthday = new DateTime(1997, 12, 25),
                PhoneNumber = "0122265899",
                CustomerAddress = "237 Bế Văn Đán, P11, Q5, TP.HCM"
            });*/
        }
    }
}
