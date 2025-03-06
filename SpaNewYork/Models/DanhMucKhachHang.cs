using System.ComponentModel.DataAnnotations;

namespace SpaNewYork.Models
{
    public class DanhMucKhachHang
    {
        [Key]
        public int STT { get; set; }

        [Required, StringLength(255)]
        public string LoaiKhachHang { get; set; }

        [Required, StringLength(50)]
        public string MaKhachHang { get; set; }

        [Required, StringLength(255)]
        public string TenKhachHang { get; set; }

        [Required, StringLength(20)]
        public string SoDienThoai { get; set; }

        [Required, StringLength(255)]
        public string DiaChi { get; set; }

        [StringLength(255)]
        public string GhiChu { get; set; }

        [StringLength(255)]
        public string NgaySinh { get; set; }
    }
}
