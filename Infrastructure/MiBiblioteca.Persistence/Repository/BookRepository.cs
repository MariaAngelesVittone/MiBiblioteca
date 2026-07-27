using Microsoft.EntityFrameworkCore;
using MiBiblioteca.Application.Interfaces.Repositories;
using MiBiblioteca.Domain.Entities;
using MiBiblioteca.Persistence.Context;

namespace MiBiblioteca.Persistence.Repository
{
    public class BookRepository : RepositoryBase<Book, Guid>, IBookRepository
    {
        public BookRepository(MiBibliotecaContext context) : base(context) { }

        public async Task<Book?> GetByIsbnAsync(string isbn) =>
            await _dbSet.FirstOrDefaultAsync(b => b.Isbn == isbn);
    }
}
