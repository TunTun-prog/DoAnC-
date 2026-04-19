using System.Collections.Generic;

namespace AdminSystem.Models
{
    public class HeatmapResponse
    {
        // dates in yyyy-MM-dd format
        public List<string> Dates { get; set; } = new();
        // kiosk display names (address or name) — kept for compatibility
        public List<string> KioskNames { get; set; } = new();
        // matrix[kioskIndex][dateIndex] = count
        public List<List<int>> Matrix { get; set; } = new();
        // totals per date (sum across kiosks)
        public List<int> Totals { get; set; } = new();

        // per-kiosk metadata for map:
        public List<KioskPoint> KioskPoints { get; set; } = new();
    }

    public class KioskPoint
    {
        public int Id { get; set; }
        // QuayHang.Ten (display name for charts)
        public string Name { get; set; } = string.Empty;
        // QuayHang.DiaChi (address)
        public string? Address { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        // total visits for the selected date range
        public int Total { get; set; }
    }
}