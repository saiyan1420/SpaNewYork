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
    public class BaiVietController : Controller
    {
        private readonly SpaNewYorkDB _context;

        public BaiVietController(SpaNewYorkDB context)
        {
            _context = context;
        }

        // GET: BaiViet
        public async Task<IActionResult> Index()
        {
            var spaNewYorkDB = _context.BaiViet.Include(b => b.ChuDe).Include(b => b.NguoiDung);
            return View(await spaNewYorkDB.ToListAsync());
        }

        // GET: BaiViet/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var baiViet = await _context.BaiViet
                .Include(b => b.ChuDe)
                .Include(b => b.NguoiDung)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (baiViet == null)
            {
                return NotFound();
            }

            return View(baiViet);
        }

        // GET: BaiViet/Create
        public IActionResult Create()
        {
            ViewData["ChuDeID"] = new SelectList(_context.Set<ChuDe>(), "ID", "TenChuDe");
            ViewData["NguoiDungID"] = new SelectList(_context.NguoiDung, "ID", "Email");
            return View();
        }

        // POST: BaiViet/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,ChuDeID,NguoiDungID,TieuDe,TieuDeKhongDau,TomTat,NoiDung,NgayDang,LuotXem,KiemDuyet,HienThi")] BaiViet baiViet)
        {
            if (ModelState.IsValid)
            {
                _context.Add(baiViet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ChuDeID"] = new SelectList(_context.Set<ChuDe>(), "ID", "TenChuDe", baiViet.ChuDeID);
            ViewData["NguoiDungID"] = new SelectList(_context.NguoiDung, "ID", "Email", baiViet.NguoiDungID);
            return View(baiViet);
        }

        // GET: BaiViet/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var baiViet = await _context.BaiViet.FindAsync(id);
            if (baiViet == null)
            {
                return NotFound();
            }
            ViewData["ChuDeID"] = new SelectList(_context.Set<ChuDe>(), "ID", "TenChuDe", baiViet.ChuDeID);
            ViewData["NguoiDungID"] = new SelectList(_context.NguoiDung, "ID", "Email", baiViet.NguoiDungID);
            return View(baiViet);
        }

        // POST: BaiViet/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,ChuDeID,NguoiDungID,TieuDe,TieuDeKhongDau,TomTat,NoiDung,NgayDang,LuotXem,KiemDuyet,HienThi")] BaiViet baiViet)
        {
            if (id != baiViet.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(baiViet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BaiVietExists(baiViet.ID))
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
            ViewData["ChuDeID"] = new SelectList(_context.Set<ChuDe>(), "ID", "TenChuDe", baiViet.ChuDeID);
            ViewData["NguoiDungID"] = new SelectList(_context.NguoiDung, "ID", "Email", baiViet.NguoiDungID);
            return View(baiViet);
        }

        // GET: BaiViet/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var baiViet = await _context.BaiViet
                .Include(b => b.ChuDe)
                .Include(b => b.NguoiDung)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (baiViet == null)
            {
                return NotFound();
            }

            return View(baiViet);
        }

        // POST: BaiViet/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var baiViet = await _context.BaiViet.FindAsync(id);
            if (baiViet != null)
            {
                _context.BaiViet.Remove(baiViet);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BaiVietExists(int id)
        {
            return _context.BaiViet.Any(e => e.ID == id);
        }
    }
}
