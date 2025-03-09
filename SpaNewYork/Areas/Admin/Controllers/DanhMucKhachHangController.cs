using System;
using System.Collections.Generic;
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

        // GET: DanhMucKhachHang/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var danhMucKhachHang = await _context.DanhMucKhachHang.FirstOrDefaultAsync(m => m.STT == id);
            if (danhMucKhachHang == null) return NotFound();

            return View(danhMucKhachHang);
        }

        // GET: DanhMucKhachHang/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DanhMucKhachHang/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LoaiKhachHang,TenKhachHang,SoDienThoai,DiaChi,GhiChu,NgaySinh")] DanhMucKhachHang danhMucKhachHang)
        {
            if (ModelState.IsValid)
            {
                // Tạo mã khách hàng tự động
                var lastCustomer = _context.DanhMucKhachHang
                    .Where(kh => kh.LoaiKhachHang == danhMucKhachHang.LoaiKhachHang)
                    .OrderByDescending(kh => kh.MaKhachHang)
                    .FirstOrDefault();

                string prefix = danhMucKhachHang.LoaiKhachHang;
                int nextNumber = 1;

                if (lastCustomer != null)
                {
                    string lastCode = lastCustomer.MaKhachHang.Substring(2);
                    if (int.TryParse(lastCode, out int lastNumber))
                    {
                        nextNumber = lastNumber + 1;
                    }
                }

                danhMucKhachHang.MaKhachHang = $"{prefix}{nextNumber:D4}";

                _context.Add(danhMucKhachHang);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(danhMucKhachHang);
        }

        // GET: DanhMucKhachHang/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var danhMucKhachHang = await _context.DanhMucKhachHang.FindAsync(id);
            if (danhMucKhachHang == null) return NotFound();

            return View(danhMucKhachHang);
        }

        // POST: DanhMucKhachHang/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("STT,LoaiKhachHang,MaKhachHang,TenKhachHang,SoDienThoai,DiaChi,GhiChu,NgaySinh")] DanhMucKhachHang danhMucKhachHang)
        {
            if (id != danhMucKhachHang.STT) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
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
                return RedirectToAction(nameof(Index));
            }
            return View(danhMucKhachHang);
        }

        // GET: DanhMucKhachHang/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var danhMucKhachHang = await _context.DanhMucKhachHang
                .FirstOrDefaultAsync(m => m.STT == id);
            if (danhMucKhachHang == null) return NotFound();

            return View(danhMucKhachHang);
        }

        // POST: DanhMucKhachHang/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var danhMucKhachHang = await _context.DanhMucKhachHang.FindAsync(id);
            if (danhMucKhachHang != null)
            {
                _context.DanhMucKhachHang.Remove(danhMucKhachHang);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DanhMucKhachHangExists(int id)
        {
            return _context.DanhMucKhachHang.Any(e => e.STT == id);
        }
    }
}
