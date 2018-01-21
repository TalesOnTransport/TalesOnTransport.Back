using Microsoft.EntityFrameworkCore;

namespace TalesOnTransport.Back.Models
{
    public class ScanContext : DbContext
    {
        public ScanContext(DbContextOptions<ScanContext> options)
            : base (options)
        {
        }

        public DbSet<Scan> Scan { get; set; }
    }
}
