using Microsoft.EntityFrameworkCore;

namespace TalesOnTransport.Back.Models
{
    public class BookContext : DbContext
    {
        public BookContext(DbContextOptions<BookContext> options)
            : base(options)
        {
        }

        public BookContext() : base()
        {
        }

        public DbSet<Book> Book { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Book>()
                .HasIndex(b => b.PIN)
                .IsUnique();
        }
    }
}
