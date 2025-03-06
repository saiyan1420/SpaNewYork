using SpaNewYork.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace SpaNewYork.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class DanhMucHangHoaDichVuController : Controller
    { 
        private readonly SpaNewYorkDB _context;

        public DanhMucHangHoaDichVuController(SpaNewYorkDB context)
        {
            _context = context;
        }

        // GET: Index 
        public async Task<IActionResult> Index(string timkiem, int? trang)
        {
            var danhSach = LayDanhSachDanhMuc(trang ?? 1).DanhMucHangHoaDichVu;

            if (!danhSach.Any())
                return NotFound();

            if (!string.IsNullOrEmpty(timkiem))
            {
                danhSach = danhSach
                    .Where(n => n.TenHHDV.Contains(timkiem, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            return View(danhSach);
        }

        private PhanTrangDanhMuc LayDanhSachDanhMuc(int trangHienTai)
        {
            int maxRows = 2000;

            var danhMucQuery = _context.DanhMucHangHoaDichVu.OrderBy(r => r.STT);
            var tongSoTrang = (int)Math.Ceiling((double)danhMucQuery.Count() / maxRows);

            PhanTrangDanhMuc phanTrang = new PhanTrangDanhMuc
            {
                DanhMucHangHoaDichVu = danhMucQuery.Skip((trangHienTai - 1) * maxRows).Take(maxRows).ToList(),
                TongSoTrang = tongSoTrang,
                TrangHienTai = trangHienTai
            };

            return phanTrang;
        }

        public IActionResult ChiTiet(short id)
        {
            var danhMuc = _context.DanhMucHangHoaDichVu.SingleOrDefault(r => r.STT == id);
            if (danhMuc == null)
                return NotFound();
            return View(danhMuc);
        }

        public IActionResult PhanLoai(byte? nhomHHDV, int? trang)
        {
            var danhSachPhanLoai = LayDanhSachDanhMucTheoPhanLoai(nhomHHDV, trang ?? 1).DanhMucHangHoaDichVu;
            if (!danhSachPhanLoai.Any())
                return NotFound();
            return View(danhSachPhanLoai);
        }

        private PhanTrangDanhMuc LayDanhSachDanhMucTheoPhanLoai(byte? nhomHHDV, int trangHienTai)
        {
            int maxRows = 20;

            var danhMucPhanLoaiQuery = _context.DanhMucHangHoaDichVu
                .Where(r => !nhomHHDV.HasValue || r.NhomHHDV == nhomHHDV)
                .OrderBy(r => r.STT);

            var tongSoTrang = (int)Math.Ceiling((double)danhMucPhanLoaiQuery.Count() / maxRows);

            PhanTrangDanhMuc phanTrang = new PhanTrangDanhMuc
            {
                DanhMucHangHoaDichVu = danhMucPhanLoaiQuery.Skip((trangHienTai - 1) * maxRows).Take(maxRows).ToList(),
                TongSoTrang = tongSoTrang,
                TrangHienTai = trangHienTai
            };

            return phanTrang;
        }
    }
}
