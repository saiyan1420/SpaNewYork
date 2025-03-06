using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpaNewYork.Models
{
    public class DanhMucHangHoaDichVu
    {
        [Key]
        [DisplayName("Mã danh mục")]
        public short STT { get; set; }  

        [DisplayName("Nhóm hàng hóa & dịch vụ")]
        [Required(ErrorMessage = "Nhóm hàng hóa & dịch vụ không được bỏ trống.")]
        public byte NhomHHDV { get; set; } 

        [StringLength(50)]
        [DisplayName("Mã hàng hóa & dịch vụ")]
        [Required(ErrorMessage = "Mã hàng hóa & dịch vụ không được bỏ trống.")]
        public string MaHHDV { get; set; }  

        [StringLength(255)]
        [DisplayName("Tên hàng hóa & dịch vụ")]
        [Required(ErrorMessage = "Tên hàng hóa & dịch vụ không được bỏ trống.")]
        public string TenHHDV { get; set; }  

        [DisplayName("Giá bán")]
        [Required(ErrorMessage = "Giá bán không được bỏ trống.")]
        [DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = false)]
        public decimal GiaBan { get; set; } 

        [StringLength(50)]
        [DisplayName("Đơn vị tính")]
        [Required(ErrorMessage = "Đơn vị tính không được bỏ trống.")]
        public string DonViTinh { get; set; }  

        [DisplayName("Mô tả chi tiết")]
        [DataType(DataType.MultilineText)]
        public string? MoTa { get; set; }  

        public ICollection<DatHang_ChiTiet>? DatHang_ChiTiet { get; set; }

        [NotMapped]
        [Display(Name = "Hình ảnh sản phẩm")]
        public IFormFile? DuLieuHinhAnh { get; set; }
    }

    [NotMapped]
    public class PhanTrangDanhMuc
    {
        public int TrangHienTai { get; set; }
        public int TongSoTrang { get; set; }
        public List<DanhMucHangHoaDichVu> DanhMucHangHoaDichVu { get; set; }
        public bool HasPreviousPage => TrangHienTai > 1;
        public bool HasNextPage => TrangHienTai < TongSoTrang;
    }
}
