using System.ComponentModel.DataAnnotations;

namespace SpaNewYork.Models
{
    public class ComboDichVu
    {
        [Key]
        [StringLength(50)]
        public string SoHoaDon { get; set; }

        [StringLength(50)]
        public string MaKhachHang { get; set; }

        [StringLength(255)]
        public string TenKhachHang { get; set; }

        [Required, StringLength(20)]
        public string SoDienThoai { get; set; }

        [StringLength(50)]
        public string LoaiDichVu { get; set; }

        public string DanhSachBuoi { get; set; }
    }
}
