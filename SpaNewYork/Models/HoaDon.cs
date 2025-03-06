using System.ComponentModel.DataAnnotations;

namespace SpaNewYork.Models
{
    public class HoaDon
    {
        [Key]
        [StringLength(50)]
        public string SoHoaDon { get; set; }

        [Required]
        public DateTime NgayHoaDon { get; set; }

        [Required, StringLength(50)]
        public string MaKhachHang { get; set; }

        [StringLength(255)]
        public string TenKhachHang { get; set; }

        [Required, StringLength(10)]
        public string MaNV { get; set; }

        public decimal GiaBan { get; set; }
        public decimal PhanTramChietKhau { get; set; }
        public decimal SoTienChietKhauGiamGia { get; set; }
        public decimal TongThanhToan { get; set; }
    }
}
