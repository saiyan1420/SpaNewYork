using System.ComponentModel.DataAnnotations;
using SpaNewYork.Models;

namespace SpaNewYork.Models
{
    public class GioHang
    {
        [Key]
        [StringLength(255)]
        public string ID { get; set; }

        [StringLength(255)]
        public string TenDangNhap { get; set; }

        public int STT { get; set; }
        public int SoLuongTrongGio { get; set; }

        [Required]
        public DateTime ThoiGian { get; set; }

        public DanhMucHangHoaDichVu? SanPham { get; set; }
    }
}
