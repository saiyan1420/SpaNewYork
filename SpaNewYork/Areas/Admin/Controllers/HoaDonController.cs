using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SpaNewYork.Models;

namespace SpaNewYork.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class HoaDonController : Controller
    {
        private readonly SpaNewYorkDB _context;

        public HoaDonController(SpaNewYorkDB context)
        {
            _context = context;
        }

        // GET: Admin/HoaDon
        public async Task<IActionResult> Index()
        {
            return View(await _context.HoaDon.ToListAsync());
        }

        // GET: Admin/HoaDon/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hoaDon = await _context.HoaDon
                .FirstOrDefaultAsync(m => m.SoHoaDon == id);
            if (hoaDon == null)
            {
                return NotFound();
            }

            return View(hoaDon);
        }

        // GET: Admin/HoaDon/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/HoaDon/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SoHoaDon,NgayHoaDon,MaKhachHang,TenKhachHang,MaNV,GiaBan,PhanTramChietKhau,SoTienChietKhauGiamGia,TongThanhToan")] HoaDon hoaDon)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hoaDon);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(hoaDon);
        }

        // GET: Admin/HoaDon/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hoaDon = await _context.HoaDon.FindAsync(id);
            if (hoaDon == null)
            {
                return NotFound();
            }
            return View(hoaDon);
        }

        // POST: Admin/HoaDon/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("SoHoaDon,NgayHoaDon,MaKhachHang,TenKhachHang,MaNV,GiaBan,PhanTramChietKhau,SoTienChietKhauGiamGia,TongThanhToan")] HoaDon hoaDon)
        {
            if (id != hoaDon.SoHoaDon)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hoaDon);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HoaDonExists(hoaDon.SoHoaDon))
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
            return View(hoaDon);
        }

        // GET: Admin/HoaDon/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hoaDon = await _context.HoaDon
                .FirstOrDefaultAsync(m => m.SoHoaDon == id);
            if (hoaDon == null)
            {
                return NotFound();
            }

            return View(hoaDon);
        }

        // POST: Admin/HoaDon/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var hoaDon = await _context.HoaDon.FindAsync(id);
            if (hoaDon != null)
            {
                _context.HoaDon.Remove(hoaDon);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HoaDonExists(string id)
        {
            return _context.HoaDon.Any(e => e.SoHoaDon == id);
        }
    }
}
