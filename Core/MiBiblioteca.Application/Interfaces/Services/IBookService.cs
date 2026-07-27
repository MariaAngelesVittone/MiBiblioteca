using MiBiblioteca.Application.Dto;

namespace MiBiblioteca.Application.Interfaces.Services
{
    public interface IBookService
    {
        Task<IEnumerable<BookResponseDto>> GetAllAsync();
        Task<BookResponseDto?> GetByIdAsync(Guid id);

        // Lanza InvalidOperationException si el ISBN ya existe, igual que
        // AuthService.RegisterAsync con un username duplicado - el mismo
        // patron para el mismo tipo de error en toda la Application layer.
        Task<BookResponseDto> CreateAsync(CreateBookDto dto);

        // Devuelve null si Open Library no tiene datos para ese ISBN (no es
        // un error, simplemente no hay nada que crear).
        Task<BookResponseDto?> CreateFromIsbnAsync(string isbn);
    }
}
