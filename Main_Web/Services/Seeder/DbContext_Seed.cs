using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Seeder.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Seeder
{
    public class DbContext_Seed
    {
        public static async Task SeedAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<DockerDemoContext>();

                using (context)
                {
                    // Create database if not exists
                    context.Database.Migrate();

                    // Create tables if not exists
                    try
                    {
                        RelationalDatabaseCreator databaseCreator = (RelationalDatabaseCreator)context.Database.GetService<IDatabaseCreator>();
                        databaseCreator.CreateTables();
                    }
                    catch (SqlException ex)
                    {
                        ex.Message.ToString();
                    }

                    // Check if a table has any data
                    if (!context.KhachHang.Any())
                    {
                        context.KhachHang.AddRange(GenerateKhachHangData());

                        await context.SaveChangesAsync();
                    }

                    if (!context.SanPham.Any())
                    {
                        context.SanPham.AddRange(GenerateSanPhamData());

                        await context.SaveChangesAsync();
                    }
                }
            }
        }

        static IEnumerable<KhachHang> GenerateKhachHangData()
        {
            return new List<KhachHang>()
            {
                new KhachHang() { MaKh = "KH001", TenKh = "Trần Văn Bé", GioiTinh = "Nam",
                    NgaySinh = new DateTime(1997, 12, 25), Sdt = "0122265899",
                    DiaChi = "237 Bế Văn Đán, P11, Q5, TP.HCM" },
                new KhachHang() { MaKh = "KH002", TenKh = "Nguyễn Thị Thơm", GioiTinh = "Nữ",
                    NgaySinh = new DateTime(1996, 11, 1), Sdt = "0188835496",
                    DiaChi = "137 Trần Văn Linh, P3, Q.Gò Vấp, TP.HCM" },
                new KhachHang() { MaKh = "KH003", TenKh = "Trần Cao Anh", GioiTinh = "Nam",
                    NgaySinh = new DateTime(1994, 12, 31), Sdt = "0124598736",
                    DiaChi = "29 Cao Bá Quát, P7, Q8, TP.HCM" },
                new KhachHang() { MaKh = "KH004", TenKh = "Lê Thị Oanh", GioiTinh = "Nữ",
                    NgaySinh = new DateTime(1996, 5, 24), Sdt = "0168955574",
                    DiaChi = "154 Dương Bá Trạc, P9, Q10, TP.HCM" },
                new KhachHang() { MaKh = "KH005", TenKh = "Trần Công Duy", GioiTinh = "Nam",
                    NgaySinh = new DateTime(1997, 1, 4), Sdt = "0185269471",
                    DiaChi = "10 Hồng Bàng, P4, Q3, TP.HCM" },
                new KhachHang() { MaKh = "KH006", TenKh = "Dương Hoàng Lấn", GioiTinh = "Nam",
                    NgaySinh = new DateTime(1992, 8, 26), Sdt = "0914412556",
                    DiaChi = "220 Trần Hưng Đạo, P11, Q1, TP.HCM" },
                new KhachHang() { MaKh = "KH007", TenKh = "Phan Thị Quỳnh", GioiTinh = "Nữ",
                    NgaySinh = new DateTime(1995, 3, 13), Sdt = "0162354712",
                    DiaChi = "147 Trần Phú, P2, Q9, TP.HCM" },
                new KhachHang() { MaKh = "KH008", TenKh = "Phạm Sinh Hùng", GioiTinh = "Nam",
                    NgaySinh = new DateTime(1996, 10, 30), Sdt = "0905476236",
                    DiaChi = "330 Trương Định, P12, Q2, TP.HCM" },
                new KhachHang() { MaKh = "KH009", TenKh = "Võ Tuấn Vương", GioiTinh = "Nam",
                    NgaySinh = new DateTime(1998, 2, 22), Sdt = "0188742563",
                    DiaChi = "221 Sư Vạn Hạnh, P6, Q7, TP.HCM" },
                new KhachHang() { MaKh = "KH010", TenKh = "Sơn Tùng MTP", GioiTinh = "Nam",
                    NgaySinh = new DateTime(1990, 4, 18), Sdt = "0918936517",
                    DiaChi = "39 Nguyễn Trãi, P7, Q8, TP.HCM" }
            };
        }

        static IEnumerable<SanPham> GenerateSanPhamData()
        {
            return new List<SanPham>()
            {
                new SanPham() { MaSp = "SP001", TenSp = "Búp bê Kumie", Dvt = "Cái",
                    DonGia = 120000, Slton = 21 },
                new SanPham() { MaSp = "SP002", TenSp = "Máy bay vận thăng Mỹ", Dvt = "Chiếc",
                    DonGia = 340000, Slton = 31 },
                new SanPham() { MaSp = "SP003", TenSp = "Trò chơi sudoku Nhật Bản", Dvt = "Bộ",
                    DonGia = 78000, Slton = 11 },
                new SanPham() { MaSp = "SP004", TenSp = "Thẻ bài magic", Dvt = "Bộ",
                    DonGia = 57000, Slton = 38 },
                new SanPham() { MaSp = "SP005", TenSp = "Gấu bông Teddy King", Dvt = "Cái",
                    DonGia = 173000, Slton = 2 },
                new SanPham() { MaSp = "SP006", TenSp = "Thẻ bài đồ chơi Pokemon", Dvt = "Bộ",
                    DonGia = 790000, Slton = 31 },
                new SanPham() { MaSp = "SP007", TenSp = "Yoyo đồ chơi bóng tối", Dvt = "Cái",
                    DonGia = 53000, Slton = 44 },
                new SanPham() { MaSp = "SP008", TenSp = "Búp bê đồ chơi Annie", Dvt = "Cái",
                    DonGia = 156000, Slton = 19 },
                new SanPham() { MaSp = "SP009", TenSp = "Gấu trúc đồ chơi bông", Dvt = "Cái",
                    DonGia = 230000, Slton = 49 },
                new SanPham() { MaSp = "SP010", TenSp = "Card bài phát sáng ma thuật", Dvt = "Bộ",
                    DonGia = 170000, Slton = 24 }
            };
        }
    }
}
