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
   
    //Test 2 cái
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ComboDichVuController : Controller
    {
        private readonly SpaNewYorkDB _context;

        public ComboDichVuController(SpaNewYorkDB context)
        {
            _context = context;
        }

        // GET: ComboDichVu
        public async Task<IActionResult> Index()
        {
            return View(await _context.ComboDichVu.ToListAsync());
        }

        // GET: ComboDichVu/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comboDichVu = await _context.ComboDichVu
                .FirstOrDefaultAsync(m => m.SoHoaDon == id);
            if (comboDichVu == null)
            {
                return NotFound();
            }

            return View(comboDichVu);
        }

        // GET: ComboDichVu/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ComboDichVu/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SoHoaDon,MaKhachHang,TenKhachHang,SoDienThoai,LoaiDichVu,DanhSachBuoi")] ComboDichVu comboDichVu)
        {
            if (ModelState.IsValid)
            {
                _context.Add(comboDichVu);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(comboDichVu);
        }

        // GET: ComboDichVu/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comboDichVu = await _context.ComboDichVu.FindAsync(id);
            if (comboDichVu == null)
            {
                return NotFound();
            }
            return View(comboDichVu);
        }

        // POST: ComboDichVu/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("SoHoaDon,MaKhachHang,TenKhachHang,SoDienThoai,LoaiDichVu,DanhSachBuoi")] ComboDichVu comboDichVu)
        {
            if (id != comboDichVu.SoHoaDon)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(comboDichVu);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComboDichVuExists(comboDichVu.SoHoaDon))
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
            return View(comboDichVu);
        }

        // GET: ComboDichVu/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comboDichVu = await _context.ComboDichVu
                .FirstOrDefaultAsync(m => m.SoHoaDon == id);
            if (comboDichVu == null)
            {
                return NotFound();
            }

            return View(comboDichVu);
        }

        // POST: ComboDichVu/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var comboDichVu = await _context.ComboDichVu.FindAsync(id);
            if (comboDichVu != null)
            {
                _context.ComboDichVu.Remove(comboDichVu);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ComboDichVuExists(string id)
        {
            return _context.ComboDichVu.Any(e => e.SoHoaDon == id);
        }
    }
}
