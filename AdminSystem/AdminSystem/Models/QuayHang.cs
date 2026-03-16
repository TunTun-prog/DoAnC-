using System;

namespace AdminSystem.Models
{
    public class QuayHang
    {
        public int Id { get; set; }

        public string Ten { get; set; } = null!;
        public string MoTa { get; set; }
        public string ViTri { get; set; }
        public string NguoiQuanLy { get; set; }

        public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;
    }
}