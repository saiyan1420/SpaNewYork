using System.ComponentModel.DataAnnotations;

namespace SpaNewYork.Models
{
    public class NhanVien
    {
        [Key]
        [StringLength(10)]
        public string MaNV { get; set; }

        [Required, StringLength(100)]
        public string TenNhanVien { get; set; }
    }
}
