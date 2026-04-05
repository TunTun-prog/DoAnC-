using System;

namespace AdminSystem.Models
{
    public class QuayHang
    {
        public int Id { get; set; }

        public string Ten { get; set; } = null!;
        public string MoTa { get; set; } = string.Empty;
        public string ViTri { get; set; } = string.Empty;
        public string NguoiQuanLy { get; set; } = string.Empty;

        // Location fields
        public string? DiaChi { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }

        // Cover image path (relative, e.g. /uploads/quayhangs/xxx.jpg)
        public string? CoverImagePath { get; set; }

        // Audio narration file path (relative, e.g. /uploads/quayhangs/audio/xxx.mp3)
        public string? AudioPath { get; set; }

        public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;
    }
}