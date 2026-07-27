using MiBiblioteca.Application.Interfaces.Repositories;
using MiBiblioteca.Domain.Entities;
using MiBiblioteca.Persistence.Context;

namespace MiBiblioteca.Persistence.Repository
{
    public class ReadingEntryRepository : RepositoryBase<ReadingEntry, Guid>, IReadingEntryRepository
    {
        public ReadingEntryRepository(MiBibliotecaContext context) : base(context) { }
    }
}
