using System;

namespace AdminSystem.Models
{
    public class AccessLog
    {
        public int Id { get; set; }
        public DateTime VisitTime { get; set; }
        public string IpAddress { get; set; }
        
        // Foreign key to QuayHang.Id
        public int QuayHangId { get; set; }
        public QuayHang? QuayHang { get; set; }

        public string Language { get; set; }
    }
}