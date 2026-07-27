namespace MiBiblioteca.Application.Interfaces.Repositories
{
    // Agrupa todos los repositorios y expone un unico SaveChanges, para que
    // una operacion que toque mas de una entidad se guarde como una sola
    // transaccion (por ejemplo: crear un ReadingEntry y actualizar el Book).
    public interface IUnitOfWork
    {
        IBookRepository Books { get; }
        IUserRepository Users { get; }
        IReadingEntryRepository ReadingEntries { get; }

        int SaveChanges();
        Task<int> SaveChangesAsync();
    }
}
