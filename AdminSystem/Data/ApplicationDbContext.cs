using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AdminSystem.Models;

namespace AdminSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<QuayHang> QuayHangs { get; set; }
        public DbSet<AccessLog> AccessLogs { get; set; }
    }
}
