using System;
using System.Collections.Generic;

namespace Seeder.Models
{
    public partial class SanPham
    {
        public string MaSp { get; set; }
        public string TenSp { get; set; }
        public string Dvt { get; set; }
        public decimal? DonGia { get; set; }
        public int? Slton { get; set; }
    }
}
