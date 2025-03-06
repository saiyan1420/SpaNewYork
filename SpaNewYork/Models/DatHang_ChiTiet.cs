using SpaNewYork.Models;

namespace SpaNewYork.Models
{
    public class DatHang_ChiTiet
    {
        public int ID { get; set; }
        public int DatHangID { get; set; }
        public int MaHHDV { get; set; }
        public short SoLuong { get; set; }
        public decimal DonGia { get; set; }

        public DatHang? DatHang { get; set; }
        public DanhMucHangHoaDichVu? DanhMucHangHoaDichVu { get; set; }
    }
}
