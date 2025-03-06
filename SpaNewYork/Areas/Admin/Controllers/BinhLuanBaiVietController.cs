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
    public class BinhLuanBaiVietController : Controller
    {
        private readonly SpaNewYorkDB _context;

        public BinhLuanBaiVietController(SpaNewYorkDB context)
        {
            _context = context;
        }

        // GET: BinhLuanBaiViet
        public async Task<IActionResult> Index()
        {
            var spaNewYorkDB = _context.BinhLuanBaiViet.Include(b => b.BaiViet).Include(b => b.NguoiDung);
            return View(await spaNewYorkDB.ToListAsync());
        }

        // GET: BinhLuanBaiViet/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var binhLuanBaiViet = await _context.BinhLuanBaiViet
                .Include(b => b.BaiViet)
                .Include(b => b.NguoiDung)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (binhLuanBaiViet == null)
            {
                return NotFound();
            }

            return View(binhLuanBaiViet);
        }

        // GET: BinhLuanBaiViet/Create
        public IActionResult Create()
        {
            ViewData["BaiVietID"] = new SelectList(_context.BaiViet, "ID", "NoiDung");
            ViewData["NguoiDungID"] = new SelectList(_context.NguoiDung, "ID", "Email");
            return View();
        }

        // POST: BinhLuanBaiViet/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,BaiVietID,NguoiDungID,NoiDungBinhLuan,NgayDang,LuotXem,KiemDuyet")] BinhLuanBaiViet binhLuanBaiViet)
        {
            if (ModelState.IsValid)
            {
                _context.Add(binhLuanBaiViet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BaiVietID"] = new SelectList(_context.BaiViet, "ID", "NoiDung", binhLuanBaiViet.BaiVietID);
            ViewData["NguoiDungID"] = new SelectList(_context.NguoiDung, "ID", "Email", binhLuanBaiViet.NguoiDungID);
            return View(binhLuanBaiViet);
        }

        // GET: BinhLuanBaiViet/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var binhLuanBaiViet = await _context.BinhLuanBaiViet.FindAsync(id);
            if (binhLuanBaiViet == null)
            {
                return NotFound();
            }
            ViewData["BaiVietID"] = new SelectList(_context.BaiViet, "ID", "NoiDung", binhLuanBaiViet.BaiVietID);
            ViewData["NguoiDungID"] = new SelectList(_context.NguoiDung, "ID", "Email", binhLuanBaiViet.NguoiDungID);
            return View(binhLuanBaiViet);
        }

        // POST: BinhLuanBaiViet/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,BaiVietID,NguoiDungID,NoiDungBinhLuan,NgayDang,LuotXem,KiemDuyet")] BinhLuanBaiViet binhLuanBaiViet)
        {
            if (id != binhLuanBaiViet.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(binhLuanBaiViet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BinhLuanBaiVietExists(binhLuanBaiViet.ID))
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
            ViewData["BaiVietID"] = new SelectList(_context.BaiViet, "ID", "NoiDung", binhLuanBaiViet.BaiVietID);
            ViewData["NguoiDungID"] = new SelectList(_context.NguoiDung, "ID", "Email", binhLuanBaiViet.NguoiDungID);
            return View(binhLuanBaiViet);
        }

        // GET: BinhLuanBaiViet/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var binhLuanBaiViet = await _context.BinhLuanBaiViet
                .Include(b => b.BaiViet)
                .Include(b => b.NguoiDung)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (binhLuanBaiViet == null)
            {
                return NotFound();
            }

            return View(binhLuanBaiViet);
        }

        // POST: BinhLuanBaiViet/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var binhLuanBaiViet = await _context.BinhLuanBaiViet.FindAsync(id);
            if (binhLuanBaiViet != null)
            {
                _context.BinhLuanBaiViet.Remove(binhLuanBaiViet);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BinhLuanBaiVietExists(int id)
        {
            return _context.BinhLuanBaiViet.Any(e => e.ID == id);
        }
    }
}
