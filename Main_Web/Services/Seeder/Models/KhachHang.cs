using System;
using System.Collections.Generic;

namespace Seeder.Models
{
    public partial class KhachHang
    {
        public string MaKh { get; set; }
        public string TenKh { get; set; }
        public string GioiTinh { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string Sdt { get; set; }
        public string DiaChi { get; set; }
    }
}
