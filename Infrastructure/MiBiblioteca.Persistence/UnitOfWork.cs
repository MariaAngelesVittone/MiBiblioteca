using MiBiblioteca.Application.Interfaces.Repositories;
using MiBiblioteca.Persistence.Context;
using MiBiblioteca.Persistence.Repository;

namespace MiBiblioteca.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MiBibliotecaContext _context;

        public UnitOfWork(MiBibliotecaContext context)
        {
            _context = context;
            Books = new BookRepository(_context);
            Users = new UserRepository(_context);
            ReadingEntries = new ReadingEntryRepository(_context);
        }

        public IBookRepository Books { get; }
        public IUserRepository Users { get; }
        public IReadingEntryRepository ReadingEntries { get; }

        public int SaveChanges() => _context.SaveChanges();

        public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();
    }
}
