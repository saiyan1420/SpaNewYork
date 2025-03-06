using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SpaNewYork.Models
{
    public class DatHang
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public int NguoiDungID { get; set; }

        [Required]
        public int TinhTrangID { get; set; }

        [Required, StringLength(20)]
        public string DienThoaiGiaoHang { get; set; }

        [Required, StringLength(255)]
        public string DiaChiGiaoHang { get; set; }

        [Required]
        public DateTime NgayDatHang { get; set; }

        public NguoiDung? NguoiDung { get; set; }
        public TinhTrang? TinhTrang { get; set; }
        public ICollection<DatHang_ChiTiet>? DatHang_ChiTiet { get; set; }
    }
}
