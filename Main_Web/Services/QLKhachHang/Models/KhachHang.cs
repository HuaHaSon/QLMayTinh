using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QLKhachHang.Models
{
    public partial class KhachHang
    {
        [Display(Name = "Mã khách hàng")]
        public string MaKh { get; set; }

        [StringLength(50, ErrorMessage = "Tên không được dài hơn 50 ký tự.")]
        [Display(Name = "Tên khách hàng")]
        public string TenKh { get; set; }

        [StringLength(5, ErrorMessage = "Không được dài hơn 5 ký tự.")]
        [Display(Name = "Giới tính")]
        public string GioiTinh { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Ngày sinh")]
        public DateTime? NgaySinh { get; set; }

        [StringLength(10, ErrorMessage = "Số điện thoại không được dài hơn 10 chữ số.")]
        [Display(Name = "Số điện thoại")]
        public string Sdt { get; set; }

        [Display(Name = "Địa chỉ")]
        public string DiaChi { get; set; }
    }
}
