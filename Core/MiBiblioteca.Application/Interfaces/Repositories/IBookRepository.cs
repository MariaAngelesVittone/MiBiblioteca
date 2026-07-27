using MiBiblioteca.Domain.Entities;

namespace MiBiblioteca.Application.Interfaces.Repositories
{
    public interface IBookRepository : IRepositoryBase<Book, Guid>
    {
        // Metodo especifico de Book que no aplica a cualquier entidad,
        // por eso no vive en el repository generico.
        Task<Book?> GetByIsbnAsync(string isbn);
    }
}
