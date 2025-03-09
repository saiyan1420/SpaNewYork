using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpaNewYork.Models;

namespace SpaNewYork.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class DanhMucKhachHangController : Controller
    {
        private readonly SpaNewYorkDB _context;

        public DanhMucKhachHangController(SpaNewYorkDB context)
        {
            _context = context;
        }

        // GET: DanhMucKhachHang
        public async Task<IActionResult> Index()
        {
            return View(await _context.DanhMucKhachHang.ToListAsync());
        }

        // GET: DanhMucKhachHang/Create
        public IActionResult Create()
        {
            return View();
        }

        // HÀM SINH MÃ KHÁCH HÀNG TỰ ĐỘNG
        private string GenerateCustomerCode(string loaiKH)
        {
            string prefix = loaiKH == "VIP" ? "VIP" : "KL";

            var lastCustomer = _context.DanhMucKhachHang
                .Where(kh => kh.LoaiKhachHang == loaiKH)
                .OrderByDescending(kh => kh.MaKhachHang)
                .FirstOrDefault();

            int nextNumber = 1; // Mặc định nếu chưa có khách nào

            if (lastCustomer != null)
            {
                string lastCode = lastCustomer.MaKhachHang.Substring(2);
                if (int.TryParse(lastCode, out int lastNumber))
                {
                    nextNumber = lastNumber + 1;
                }
            }

            return $"{prefix}{nextNumber:D4}";
        }

        // POST: DanhMucKhachHang/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LoaiKhachHang,TenKhachHang,SoDienThoai,DiaChi,GhiChu,NgaySinh")] DanhMucKhachHang danhMucKhachHang)
        {
            if (ModelState.IsValid)
            {
                danhMucKhachHang.MaKhachHang = GenerateCustomerCode(danhMucKhachHang.LoaiKhachHang);
                _context.Add(danhMucKhachHang);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(danhMucKhachHang);
        }

        // API lấy mã khách hàng mới
        [HttpGet]
        public IActionResult GetNextCustomerCode(string loaiKH)
        {
            string newCode = GenerateCustomerCode(loaiKH);
            return Content(newCode);
        }
    }
}
