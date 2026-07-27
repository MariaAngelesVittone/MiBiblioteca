using MiBiblioteca.Application.Dto;

namespace MiBiblioteca.Application.Interfaces.Services
{
    // Application solo sabe que "alguien" le puede dar metadata de un libro
    // por ISBN. No sabe que ese "alguien" es Open Library ni que viaja por
    // HTTP - eso es un detalle de Infrastructure (MiBiblioteca.ExternalServices).
    public interface IBookMetadataProvider
    {
        Task<BookMetadataDto?> GetByIsbnAsync(string isbn);
    }
}
