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
    //test cong no
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CongNoKhachHangController : Controller
    {
        private readonly SpaNewYorkDB _context;

        public CongNoKhachHangController(SpaNewYorkDB context)
        {
            _context = context;
        }

        // GET: CongNoKhachHang
        public async Task<IActionResult> Index()
        {
            return View(await _context.CongNoKhachHang.ToListAsync());
        }

        // GET: CongNoKhachHang/Details/5
        public async Task<IActionResult> Details(double? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var congNoKhachHang = await _context.CongNoKhachHang
                .FirstOrDefaultAsync(m => m.STT == id);
            if (congNoKhachHang == null)
            {
                return NotFound();
            }

            return View(congNoKhachHang);
        }

        // GET: CongNoKhachHang/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CongNoKhachHang/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("STT,Ngay,TenKhachHang,SoHD,DiaChi,SoDienThoai,NoiDung,TongPhaiTraNguoiBan,TongPhaiThuKH,DaTToan,ConLaiPhaiTT,DaTTDu")] CongNoKhachHang congNoKhachHang)
        {
            if (ModelState.IsValid)
            {
                _context.Add(congNoKhachHang);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(congNoKhachHang);
        }

        // GET: CongNoKhachHang/Edit/5
        public async Task<IActionResult> Edit(double? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var congNoKhachHang = await _context.CongNoKhachHang.FindAsync(id);
            if (congNoKhachHang == null)
            {
                return NotFound();
            }
            return View(congNoKhachHang);
        }

        // POST: CongNoKhachHang/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(double id, [Bind("STT,Ngay,TenKhachHang,SoHD,DiaChi,SoDienThoai,NoiDung,TongPhaiTraNguoiBan,TongPhaiThuKH,DaTToan,ConLaiPhaiTT,DaTTDu")] CongNoKhachHang congNoKhachHang)
        {
            if (id != congNoKhachHang.STT)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(congNoKhachHang);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CongNoKhachHangExists(congNoKhachHang.STT))
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
            return View(congNoKhachHang);
        }

        // GET: CongNoKhachHang/Delete/5
        public async Task<IActionResult> Delete(double? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var congNoKhachHang = await _context.CongNoKhachHang
                .FirstOrDefaultAsync(m => m.STT == id);
            if (congNoKhachHang == null)
            {
                return NotFound();
            }

            return View(congNoKhachHang);
        }

        // POST: CongNoKhachHang/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(double id)
        {
            var congNoKhachHang = await _context.CongNoKhachHang.FindAsync(id);
            if (congNoKhachHang != null)
            {
                _context.CongNoKhachHang.Remove(congNoKhachHang);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CongNoKhachHangExists(double id)
        {
            return _context.CongNoKhachHang.Any(e => e.STT == id);
        }
    }
}
