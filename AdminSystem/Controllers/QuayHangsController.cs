using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AdminSystem.Data;
using AdminSystem.Models;

namespace AdminSystem.Controllers
{
    [Authorize]
    public class QuayHangsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;

        public QuayHangsController(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: QuayHangs
        public async Task<IActionResult> Index()
        {
            return View(await _context.QuayHangs.ToListAsync());
        }

        // GET: QuayHangs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var quayHang = await _context.QuayHangs.FirstOrDefaultAsync(m => m.Id == id);
            if (quayHang == null) return NotFound();

            return View(quayHang);
        }

        // GET: QuayHangs/Create
        public IActionResult Create() => View();

        // POST: QuayHangs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Ten,MoTa,ViTri,NguoiQuanLy,DiaChi,Latitude,Longitude,CreatedUtc")] QuayHang quayHang, IFormFile? coverImage, IFormFile? audioFile)
        {
            // Cover image
            if (coverImage != null && coverImage.Length > 0)
            {
                var uploads = Path.Combine(_env.WebRootPath ?? "wwwroot", "uploads", "quayhangs");
                Directory.CreateDirectory(uploads);
                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(coverImage.FileName)}";
                var filePath = Path.Combine(uploads, fileName);
                await using (var fs = new FileStream(filePath, FileMode.Create))
                {
                    await coverImage.CopyToAsync(fs);
                }
                quayHang.CoverImagePath = "/uploads/quayhangs/" + fileName;
            }

            // Audio file
            if (audioFile != null && audioFile.Length > 0)
            {
                var uploadsAudio = Path.Combine(_env.WebRootPath ?? "wwwroot", "uploads", "quayhangs", "audio");
                Directory.CreateDirectory(uploadsAudio);
                var audioName = $"{Guid.NewGuid()}{Path.GetExtension(audioFile.FileName)}";
                var audioPath = Path.Combine(uploadsAudio, audioName);
                await using (var fs = new FileStream(audioPath, FileMode.Create))
                {
                    await audioFile.CopyToAsync(fs);
                }
                quayHang.AudioPath = "/uploads/quayhangs/audio/" + audioName;
            }

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
            if (id == null) return NotFound();
            var quayHang = await _context.QuayHangs.FindAsync(id);
            if (quayHang == null) return NotFound();
            return View(quayHang);
        }

        // POST: QuayHangs/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Ten,MoTa,ViTri,NguoiQuanLy,DiaChi,Latitude,Longitude,CreatedUtc,CoverImagePath,AudioPath")] QuayHang model, IFormFile? coverImage, IFormFile? audioFile)
        {
            if (id != model.Id) return NotFound();

            var quayHang = await _context.QuayHangs.FindAsync(id);
            if (quayHang == null) return NotFound();

            // update fields
            quayHang.Ten = model.Ten;
            quayHang.MoTa = model.MoTa;
            quayHang.ViTri = model.ViTri;
            quayHang.NguoiQuanLy = model.NguoiQuanLy;
            quayHang.DiaChi = model.DiaChi;
            quayHang.Latitude = model.Latitude;
            quayHang.Longitude = model.Longitude;
            quayHang.CreatedUtc = model.CreatedUtc;

            // Replace cover image
            if (coverImage != null && coverImage.Length > 0)
            {
                if (!string.IsNullOrEmpty(quayHang.CoverImagePath))
                {
                    var existing = Path.Combine(_env.WebRootPath ?? "wwwroot", quayHang.CoverImagePath.TrimStart('/').Replace('/', Path.DirectorySeparatorChar));
                    if (System.IO.File.Exists(existing)) System.IO.File.Delete(existing);
                }
                var uploads = Path.Combine(_env.WebRootPath ?? "wwwroot", "uploads", "quayhangs");
                Directory.CreateDirectory(uploads);
                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(coverImage.FileName)}";
                var filePath = Path.Combine(uploads, fileName);
                await using (var fs = new FileStream(filePath, FileMode.Create))
                {
                    await coverImage.CopyToAsync(fs);
                }
                quayHang.CoverImagePath = "/uploads/quayhangs/" + fileName;
            }

            // Replace audio file
            if (audioFile != null && audioFile.Length > 0)
            {
                if (!string.IsNullOrEmpty(quayHang.AudioPath))
                {
                    var existingA = Path.Combine(_env.WebRootPath ?? "wwwroot", quayHang.AudioPath.TrimStart('/').Replace('/', Path.DirectorySeparatorChar));
                    if (System.IO.File.Exists(existingA)) System.IO.File.Delete(existingA);
                }
                var uploadsAudio = Path.Combine(_env.WebRootPath ?? "wwwroot", "uploads", "quayhangs", "audio");
                Directory.CreateDirectory(uploadsAudio);
                var audioName = $"{Guid.NewGuid()}{Path.GetExtension(audioFile.FileName)}";
                var audioPath = Path.Combine(uploadsAudio, audioName);
                await using (var fs = new FileStream(audioPath, FileMode.Create))
                {
                    await audioFile.CopyToAsync(fs);
                }
                quayHang.AudioPath = "/uploads/quayhangs/audio/" + audioName;
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
                    if (!_context.QuayHangs.Any(e => e.Id == quayHang.Id)) return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: QuayHangs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var quayHang = await _context.QuayHangs.FirstOrDefaultAsync(m => m.Id == id);
            if (quayHang == null) return NotFound();

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
                // delete cover image
                if (!string.IsNullOrEmpty(quayHang.CoverImagePath))
                {
                    var existing = Path.Combine(_env.WebRootPath ?? "wwwroot", quayHang.CoverImagePath.TrimStart('/').Replace('/', Path.DirectorySeparatorChar));
                    if (System.IO.File.Exists(existing)) System.IO.File.Delete(existing);
                }

                // delete audio
                if (!string.IsNullOrEmpty(quayHang.AudioPath))
                {
                    var existingA = Path.Combine(_env.WebRootPath ?? "wwwroot", quayHang.AudioPath.TrimStart('/').Replace('/', Path.DirectorySeparatorChar));
                    if (System.IO.File.Exists(existingA)) System.IO.File.Delete(existingA);
                }

                _context.QuayHangs.Remove(quayHang);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuayHangExists(int id) => _context.QuayHangs.Any(e => e.Id == id);
    }
}
