using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SpaNewYork.Models;

namespace SpaNewYork.Controllers
{
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
            if (id == null)
            {
                return NotFound();
            }

            var danhMucKhachHang = await _context.DanhMucKhachHang
                .FirstOrDefaultAsync(m => m.STT == id);
            if (danhMucKhachHang == null)
            {
                return NotFound();
            }

            return View(danhMucKhachHang);
        }

        // GET: DanhMucKhachHang/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DanhMucKhachHang/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("STT,LoaiKhachHang,MaKhachHang,TenKhachHang,SoDienThoai,DiaChi,GhiChu,NgaySinh")] DanhMucKhachHang danhMucKhachHang)
        {
            if (ModelState.IsValid)
            {
                _context.Add(danhMucKhachHang);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(danhMucKhachHang);
        }

        // GET: DanhMucKhachHang/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var danhMucKhachHang = await _context.DanhMucKhachHang.FindAsync(id);
            if (danhMucKhachHang == null)
            {
                return NotFound();
            }
            return View(danhMucKhachHang);
        }

        // POST: DanhMucKhachHang/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("STT,LoaiKhachHang,MaKhachHang,TenKhachHang,SoDienThoai,DiaChi,GhiChu,NgaySinh")] DanhMucKhachHang danhMucKhachHang)
        {
            if (id != danhMucKhachHang.STT)
            {
                return NotFound();
            }

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
            if (id == null)
            {
                return NotFound();
            }

            var danhMucKhachHang = await _context.DanhMucKhachHang
                .FirstOrDefaultAsync(m => m.STT == id);
            if (danhMucKhachHang == null)
            {
                return NotFound();
            }

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
