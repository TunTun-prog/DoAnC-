using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AdminSystem.Data;
using AdminSystem.Models;
using Microsoft.AspNetCore.Authorization;

namespace AdminSystem.Controllers
{
    [Authorize]
    public class QuayHangsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public QuayHangsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: QuayHangs
        public async Task<IActionResult> Index()
        {
            return View(await _context.QuayHangs.ToListAsync());
        }

        // GET: QuayHangs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quayHang = await _context.QuayHangs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (quayHang == null)
            {
                return NotFound();
            }

            return View(quayHang);
        }

        // GET: QuayHangs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: QuayHangs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Ten,MoTa,ViTri,NguoiQuanLy,CreatedUtc")] QuayHang quayHang)
        {
            if (ModelState.IsValid)
            {
                _context.Add(quayHang);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(quayHang);
        }

        // GET: QuayHangs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quayHang = await _context.QuayHangs.FindAsync(id);
            if (quayHang == null)
            {
                return NotFound();
            }
            return View(quayHang);
        }

        // POST: QuayHangs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Ten,MoTa,ViTri,NguoiQuanLy,CreatedUtc")] QuayHang quayHang)
        {
            if (id != quayHang.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(quayHang);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuayHangExists(quayHang.Id))
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
            return View(quayHang);
        }

        // GET: QuayHangs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quayHang = await _context.QuayHangs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (quayHang == null)
            {
                return NotFound();
            }

            return View(quayHang);
        }

        // POST: QuayHangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var quayHang = await _context.QuayHangs.FindAsync(id);
            if (quayHang != null)
            {
                _context.QuayHangs.Remove(quayHang);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuayHangExists(int id)
        {
            return _context.QuayHangs.Any(e => e.Id == id);
        }
    }
}
