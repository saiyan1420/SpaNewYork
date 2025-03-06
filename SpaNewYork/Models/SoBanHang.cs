using System.ComponentModel.DataAnnotations;

namespace SpaNewYork.Models
{
    public class SoBanHang
    {
        [Required]
        public DateTime NgayGhiSo { get; set; }

        [Required]
        public DateTime NgayHoaDon { get; set; }

        [Key]
        [StringLength(50)]
        public string SoHoaDon { get; set; }

        [Required, StringLength(50)]
        public string MaHHDV { get; set; }

        public byte NhomHHDV { get; set; }

        [Required, StringLength(255)]
        public string TenHHDV { get; set; }

        [Required, StringLength(50)]
        public string MaKhachHang { get; set; }

        [Required, StringLength(255)]
        public string TenKhachHang { get; set; }

        [Required, StringLength(255)]
        public string LoaiKhachHang { get; set; }

        [Required, StringLength(10)]
        public string MaNV { get; set; }

        [Required, StringLength(255)]
        public string DonViTinh { get; set; }

        public float SoLuongBan { get; set; }
        public float SoLuongTraLai { get; set; }
        public float SoLuongThucTe { get; set; }
        public decimal GiaBan { get; set; }
        public decimal ThanhTien { get; set; }
        public decimal PhanTramChietKhau { get; set; }
        public decimal SoTienChietKhauGiamGia { get; set; }
        public decimal GiaTriTraLai { get; set; }
        public decimal DoanhThuBanHang { get; set; }
        public decimal GiaVon { get; set; }
        public decimal LoiNhuan { get; set; }
        public decimal Tip { get; set; }

        [StringLength(255)]
        public string GhiChu { get; set; }
    }
}
