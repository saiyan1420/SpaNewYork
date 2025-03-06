using System.ComponentModel.DataAnnotations;

namespace SpaNewYork.Models
{
    public class CongNoKhachHang
    {
        [Key]
        public double STT { get; set; }

        [Required]
        public DateTime Ngay { get; set; }

        [Required, StringLength(255)]
        public string TenKhachHang { get; set; }

        [Required]
        public double SoHD { get; set; }

        [Required, StringLength(255)]
        public string DiaChi { get; set; }

        [Required, StringLength(20)]
        public string SoDienThoai { get; set; }

        [Required, StringLength(255)]
        public string NoiDung { get; set; }

        public double TongPhaiTraNguoiBan { get; set; }
        public double TongPhaiThuKH { get; set; }
        public double DaTToan { get; set; }
        public double ConLaiPhaiTT { get; set; }

        [StringLength(255)]
        public string DaTTDu { get; set; }
    }
}
