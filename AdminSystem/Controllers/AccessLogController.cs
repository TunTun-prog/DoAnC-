using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using AdminSystem.Data;
using AdminSystem.Models;
using System.Collections.Generic;

namespace AdminSystem.Controllers
{
    public class AccessLogController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public AccessLogController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index(string startDate, string endDate, int? kioskId, string language, int page = 1)
        {
            var logs = _dbContext.AccessLogs.AsQueryable();
            var pageSize = 10;

            // Provide kiosk list for filter dropdown
            ViewBag.QuayHangs = _dbContext.QuayHangs.OrderBy(q => q.Ten).ToList();

            // Bộ lọc theo ngày bắt đầu
            if (!string.IsNullOrEmpty(startDate) && DateTime.TryParse(startDate, out var start))
            {
                logs = logs.Where(x => x.VisitTime >= start);
            }

            // Bộ lọc theo ngày kết thúc
            if (!string.IsNullOrEmpty(endDate) && DateTime.TryParse(endDate, out var end))
            {
                end = end.AddDays(1);
                logs = logs.Where(x => x.VisitTime <= end);
            }

            // Bộ lọc theo quầy hàng (by foreign key)
            if (kioskId.HasValue)
            {
                logs = logs.Where(x => x.QuayHangId == kioskId.Value);
            }

            // Bộ lọc theo ngôn ngữ
            if (!string.IsNullOrEmpty(language))
            {
                logs = logs.Where(x => x.Language == language);
            }

            // Sắp xếp theo thời gian giảm dần
            logs = logs.OrderByDescending(x => x.VisitTime);

            // Tính tổng số trang
            var totalCount = logs.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

            // Phân trang
            var paginatedLogs = logs
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            ViewBag.StartDate = startDate;
            ViewBag.EndDate = endDate;
            ViewBag.FilterKioskId = kioskId;
            ViewBag.FilterLanguage = language;
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.PageSize = pageSize;

            return View(paginatedLogs);
        }

        [HttpGet]
        public IActionResult Stats()
        {
            return View();
        }

        // API endpoint to return aggregated data for heatmap + totals + kiosk points (coords)
        [HttpGet]
        public async Task<JsonResult> GetHeatmapData(DateTime? startDate, DateTime? endDate)
        {
            var end = endDate?.Date ?? DateTime.UtcNow.Date;
            var start = startDate?.Date ?? end.AddDays(-29);

            var query = _dbContext.AccessLogs
                .AsNoTracking()
                .Include(a => a.QuayHang)
                .Where(a => a.VisitTime.Date >= start && a.VisitTime.Date <= end);

            // load into memory to safely access navigation properties for Ten/DiaChi
            var logs = await query.ToListAsync();

            var grouped = logs
                .GroupBy(a => new
                {
                    Date = a.VisitTime.Date,
                    KioskId = a.QuayHangId,
                    Ten = a.QuayHang != null ? a.QuayHang.Ten : null,
                    DiaChi = a.QuayHang != null ? a.QuayHang.DiaChi : null,
                    Lat = a.QuayHang != null ? a.QuayHang.Latitude : null,
                    Lng = a.QuayHang != null ? a.QuayHang.Longitude : null
                })
                .Select(g => new
                {
                    Date = g.Key.Date,
                    KioskId = g.Key.KioskId,
                    Ten = g.Key.Ten,
                    DiaChi = g.Key.DiaChi,
                    Lat = g.Key.Lat,
                    Lng = g.Key.Lng,
                    Count = g.Count()
                })
                .ToList();

            var dates = Enumerable.Range(0, (end - start).Days + 1)
                .Select(i => start.AddDays(i))
                .ToList();

            var dateStrings = dates.Select(d => d.ToString("yyyy-MM-dd")).ToList();

            var kiosks = grouped
                .Select(x => new { x.KioskId, x.Ten, x.DiaChi, x.Lat, x.Lng })
                .Distinct()
                .OrderBy(x => x.Ten ?? x.DiaChi ?? x.KioskId.ToString())
                .ToList();

            var kioskNames = kiosks.Select(k => k.DiaChi ?? k.Ten ?? $"Quầy {k.KioskId}").ToList();

            var matrix = kiosks
                .Select(k =>
                    dates.Select(d =>
                    {
                        var g = grouped.FirstOrDefault(x => x.KioskId == k.KioskId && x.Date == d);
                        return g?.Count ?? 0;
                    }).ToList()
                ).ToList();

            var totals = dates
                .Select(d => grouped.Where(g => g.Date == d).Sum(g => g.Count))
                .ToList();

            var kioskPoints = kiosks.Select(k =>
            {
                var total = grouped.Where(g => g.KioskId == k.KioskId).Sum(g => g.Count);
                return new KioskPoint
                {
                    Id = k.KioskId,
                    // IMPORTANT: Name is set to QuayHang.Ten (not DiaChi)
                    Name = k.Ten ?? $"Quầy {k.KioskId}",
                    Address = k.DiaChi,
                    Latitude = k.Lat,
                    Longitude = k.Lng,
                    Total = total
                };
            }).ToList();

            var result = new HeatmapResponse
            {
                Dates = dateStrings,
                KioskNames = kioskNames,
                Matrix = matrix,
                Totals = totals,
                KioskPoints = kioskPoints
            };

            return Json(result);
        }
    }
}