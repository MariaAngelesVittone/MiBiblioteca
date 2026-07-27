using MiBiblioteca.Domain.Entities;

namespace MiBiblioteca.Application.Interfaces.Repositories
{
    public interface IReadingEntryRepository : IRepositoryBase<ReadingEntry, Guid>
    {
        // No hay metodos especificos todavia. La implementacion de la interfaz
        // generica en cada repositorio concreto es obligatoria, aunque quede
        // vacia (asi lo aclara el apunte de Repository con Generics).
    }
}
