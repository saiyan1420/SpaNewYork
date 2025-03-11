using System;
using System.ComponentModel.DataAnnotations;

namespace SpaNewYork.Models
{
    public class DanhMucKhachHang
    {
        [Key]
        public int STT { get; set; }

        [Required(ErrorMessage = "Loại khách hàng là bắt buộc")]
        [StringLength(255)]
        [Display(Name = "Loại khách hàng")]
        public string LoaiKhachHang { get; set; }

        [Required(ErrorMessage = "Mã khách hàng là bắt buộc")]
        [StringLength(50)]
        [Display(Name = "Mã khách hàng")]
        public string MaKhachHang { get; set; }

        [Required(ErrorMessage = "Tên khách hàng là bắt buộc")]
        [StringLength(255)]
        [Display(Name = "Tên khách hàng")]
        public string TenKhachHang { get; set; }

        [StringLength(20)]
        [Display(Name = "Số điện thoại")]
        public string SoDienThoai { get; set; }

        [StringLength(255)]
        [Display(Name = "Địa chỉ")]
        public string DiaChi { get; set; }

        [StringLength(255)]
        [Display(Name = "Ghi chú")]
        public string GhiChu { get; set; }

        [StringLength(255)]
        [Display(Name = "Ngày sinh")]
        public string NgaySinh { get; set; }
    }
}