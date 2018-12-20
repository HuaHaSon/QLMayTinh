using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Main_Web.Models
{
    public partial class SanPham
    {
        [Display(Name = "Mã sản phẩm")]
        public string MaSp { get; set; }

        [StringLength(50, ErrorMessage = "Tên không được dài hơn 50 ký tự.")]
        [Display(Name = "Tên sản phẩm")]
        public string TenSp { get; set; }

        [Display(Name = "Đơn vị tính")]
        public string Dvt { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Đơn giá")]
        public decimal? DonGia { get; set; }

        [Display(Name = "Số lượng tồn")]
        public int? Slton { get; set; }
    }
}
