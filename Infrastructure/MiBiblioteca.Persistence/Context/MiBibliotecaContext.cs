using Microsoft.EntityFrameworkCore;
using MiBiblioteca.Domain.Entities;

namespace MiBiblioteca.Persistence.Context
{
    public class MiBibliotecaContext : DbContext
    {
        public MiBibliotecaContext(DbContextOptions<MiBibliotecaContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<ReadingEntry> ReadingEntries { get; set; }
    }
}
