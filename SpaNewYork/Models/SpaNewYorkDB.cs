using Microsoft.EntityFrameworkCore;
using SpaNewYork.Models;

namespace SpaNewYork.Models
{
    public class SpaNewYorkDB : DbContext
    {
        public SpaNewYorkDB(DbContextOptions<SpaNewYorkDB> options) : base(options) { }

        public DbSet<NguoiDung> NguoiDung { get; set; }
        public DbSet<TinhTrang> TinhTrang { get; set; }
        public DbSet<DatHang> DatHang { get; set; }
        public DbSet<DatHang_ChiTiet> DatHang_ChiTiet { get; set; }
        public DbSet<GioHang> GioHang { get; set; }
        public DbSet<DanhMucHangHoaDichVu> DanhMucHangHoaDichVu { get; set; }
        public DbSet<DanhMucKhachHang> DanhMucKhachHang { get; set; }
        public DbSet<HoaDon> HoaDon { get; set; }
        public DbSet<NhanVien> NhanVien { get; set; }
        public DbSet<SoBanHang> SoBanHang { get; set; }
        public DbSet<ComboDichVu> ComboDichVu { get; set; }
        public DbSet<CongNoKhachHang> CongNoKhachHang { get; set; }
        public DbSet<BaiViet> BaiViet { get; set; }
        public DbSet<BinhLuanBaiViet> BinhLuanBaiViet { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<NguoiDung>().ToTable("NguoiDung");
            modelBuilder.Entity<TinhTrang>().ToTable("TinhTrang");
            modelBuilder.Entity<DatHang>().ToTable("DatHang");
            modelBuilder.Entity<DatHang_ChiTiet>().ToTable("DatHang_ChiTiet");
            modelBuilder.Entity<GioHang>().ToTable("GioHang");
            modelBuilder.Entity<DanhMucHangHoaDichVu>().ToTable("DanhMucHangHoaDichVu");
            modelBuilder.Entity<DanhMucKhachHang>().ToTable("DanhMucKhachHang");
            modelBuilder.Entity<HoaDon>().ToTable("HoaDon");
            modelBuilder.Entity<NhanVien>().ToTable("NhanVien");
            modelBuilder.Entity<SoBanHang>().ToTable("SoBanHang");
            modelBuilder.Entity<ComboDichVu>().ToTable("ComboDichVu");
            modelBuilder.Entity<CongNoKhachHang>().ToTable("CongNoKhachHang");
            modelBuilder.Entity<BaiViet>().ToTable("BaiViet");
            modelBuilder.Entity<BinhLuanBaiViet>().ToTable("BinhLuanBaiViet");
        }
        public DbSet<SpaNewYork.Models.ChuDe> ChuDe { get; set; } = default!;
    }
}