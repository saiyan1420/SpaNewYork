using System;
using System.Linq;
using System.Text.RegularExpressions;
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
            var danhSachKhachHang = await _context.DanhMucKhachHang
                .OrderBy(kh => kh.STT)
                .ToListAsync();
            return View(danhSachKhachHang);
        }

        // GET: DanhMucKhachHang/Create
        public IActionResult Create()
        {
            // Initialize a new customer with default values for optional fields
            var newCustomer = new DanhMucKhachHang
            {
                SoDienThoai = " ",
                DiaChi = " ",
                GhiChu = " ",
                NgaySinh = " "
            };

            return View(newCustomer);
        }

        // API lấy mã khách hàng tiếp theo
        [HttpGet]
        public async Task<IActionResult> GetNextCustomerCode(string loaiKH)
        {
            if (string.IsNullOrEmpty(loaiKH)) return BadRequest("Loại khách hàng không hợp lệ.");
            string newCustomerCode = await GenerateCustomerCode(loaiKH);
            return Ok(newCustomerCode);
        }

        // POST: DanhMucKhachHang/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DanhMucKhachHang danhMucKhachHang)
        {
            // Remove MaKhachHang from ModelState since we're generating it
            ModelState.Remove("MaKhachHang");

            // Remove validation for optional fields
            ModelState.Remove("SoDienThoai");
            ModelState.Remove("DiaChi");
            ModelState.Remove("GhiChu");
            ModelState.Remove("NgaySinh");

            if (ModelState.IsValid)
            {
                try
                {
                    // Tạo STT tự động
                    danhMucKhachHang.STT = await GenerateSTT();

                    // Tạo mã khách hàng tự động
                    danhMucKhachHang.MaKhachHang = await GenerateCustomerCode(danhMucKhachHang.LoaiKhachHang);

                    // Xử lý các trường có thể NULL thành chuỗi rỗng hoặc khoảng trắng
                    danhMucKhachHang.SoDienThoai = string.IsNullOrEmpty(danhMucKhachHang.SoDienThoai) ? " " : danhMucKhachHang.SoDienThoai;
                    danhMucKhachHang.DiaChi = string.IsNullOrEmpty(danhMucKhachHang.DiaChi) ? " " : danhMucKhachHang.DiaChi;
                    danhMucKhachHang.GhiChu = string.IsNullOrEmpty(danhMucKhachHang.GhiChu) ? " " : danhMucKhachHang.GhiChu;
                    danhMucKhachHang.NgaySinh = string.IsNullOrEmpty(danhMucKhachHang.NgaySinh) ? " " : danhMucKhachHang.NgaySinh;

                    _context.Add(danhMucKhachHang);
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    // Log the exception for debugging
                    Console.WriteLine($"Error creating customer: {ex.Message}");
                    ModelState.AddModelError("", "Có lỗi xảy ra khi lưu khách hàng. Vui lòng thử lại.");
                }
            }
            else
            {
                // If validation fails, make sure optional fields have default values
                danhMucKhachHang.SoDienThoai = string.IsNullOrEmpty(danhMucKhachHang.SoDienThoai) ? " " : danhMucKhachHang.SoDienThoai;
                danhMucKhachHang.DiaChi = string.IsNullOrEmpty(danhMucKhachHang.DiaChi) ? " " : danhMucKhachHang.DiaChi;
                danhMucKhachHang.GhiChu = string.IsNullOrEmpty(danhMucKhachHang.GhiChu) ? " " : danhMucKhachHang.GhiChu;
                danhMucKhachHang.NgaySinh = string.IsNullOrEmpty(danhMucKhachHang.NgaySinh) ? " " : danhMucKhachHang.NgaySinh;
            }

            return View(danhMucKhachHang);
        }

        // GET: DanhMucKhachHang/Details/{id}
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var khachHang = await _context.DanhMucKhachHang
                .FirstOrDefaultAsync(m => m.STT == id);
            if (khachHang == null)
            {
                return NotFound();
            }

            return View(khachHang);
        }

        // GET: DanhMucKhachHang/Edit/{id}
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var khachHang = await _context.DanhMucKhachHang.FindAsync(id);
            if (khachHang == null)
            {
                return NotFound();
            }
            return View(khachHang);
        }

        // POST: DanhMucKhachHang/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DanhMucKhachHang danhMucKhachHang)
        {
            if (id != danhMucKhachHang.STT)
            {
                return NotFound();
            }

            // Remove validation for optional fields
            ModelState.Remove("SoDienThoai");
            ModelState.Remove("DiaChi");
            ModelState.Remove("GhiChu");
            ModelState.Remove("NgaySinh");

            if (ModelState.IsValid)
            {
                try
                {
                    // Xử lý các trường có thể NULL thành chuỗi rỗng hoặc khoảng trắng
                    danhMucKhachHang.SoDienThoai = string.IsNullOrEmpty(danhMucKhachHang.SoDienThoai) ? " " : danhMucKhachHang.SoDienThoai;
                    danhMucKhachHang.DiaChi = string.IsNullOrEmpty(danhMucKhachHang.DiaChi) ? " " : danhMucKhachHang.DiaChi;
                    danhMucKhachHang.GhiChu = string.IsNullOrEmpty(danhMucKhachHang.GhiChu) ? " " : danhMucKhachHang.GhiChu;
                    danhMucKhachHang.NgaySinh = string.IsNullOrEmpty(danhMucKhachHang.NgaySinh) ? " " : danhMucKhachHang.NgaySinh;

                    _context.Update(danhMucKhachHang);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DanhMucKhachHangExists(danhMucKhachHang.STT))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                catch (Exception ex)
                {
                    // Log the exception for debugging
                    Console.WriteLine($"Error updating customer: {ex.Message}");
                    ModelState.AddModelError("", "Có lỗi xảy ra khi cập nhật khách hàng. Vui lòng thử lại.");
                    return View(danhMucKhachHang);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(danhMucKhachHang);
        }

        // GET: DanhMucKhachHang/Delete/{id}
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var khachHang = await _context.DanhMucKhachHang
                .FirstOrDefaultAsync(m => m.STT == id);
            if (khachHang == null)
            {
                return NotFound();
            }

            return View(khachHang);
        }

        // POST: DanhMucKhachHang/Delete/{id}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var khachHang = await _context.DanhMucKhachHang.FindAsync(id);
                if (khachHang == null)
                {
                    return NotFound();
                }

                _context.DanhMucKhachHang.Remove(khachHang);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error deleting customer: {ex.Message}");

                // Add error to ModelState and redirect back to delete confirmation page
                ModelState.AddModelError("", "Có lỗi xảy ra khi xóa khách hàng. Khách hàng này có thể đang được sử dụng trong hệ thống.");

                // Fetch the customer again to display on the view
                var khachHang = await _context.DanhMucKhachHang.FindAsync(id);
                return View(khachHang);
            }
        }

        // Hàm sinh mã khách hàng mới
        private async Task<string> GenerateCustomerCode(string loaiKH)
        {
            if (string.IsNullOrEmpty(loaiKH)) return null;

            var lastCustomer = await _context.DanhMucKhachHang
                .Where(kh => kh.MaKhachHang.StartsWith(loaiKH))
                .OrderByDescending(kh => kh.MaKhachHang)
                .Select(kh => kh.MaKhachHang)
                .FirstOrDefaultAsync();

            int nextNumber = 1; // Start with 1 if no customers exist

            if (!string.IsNullOrEmpty(lastCustomer))
            {
                // Extract all digits from the customer code
                var match = Regex.Match(lastCustomer, @"(\d+)$");
                if (match.Success)
                {
                    if (int.TryParse(match.Groups[1].Value, out int currentNumber))
                    {
                        nextNumber = currentNumber + 1;
                    }
                }
            }

            return $"{loaiKH}{nextNumber:D4}";
        }

        // Hàm sinh STT tự động
        private async Task<int> GenerateSTT()
        {
            var maxSTT = await _context.DanhMucKhachHang.MaxAsync(kh => (int?)kh.STT) ?? 0;
            return maxSTT + 1;
        }

        // Helper method to check if a customer exists
        private bool DanhMucKhachHangExists(int id)
        {
            return _context.DanhMucKhachHang.Any(e => e.STT == id);
        }
    }
}