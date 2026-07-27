using AutoMapper;
using MiBiblioteca.Application.Dto;
using MiBiblioteca.Application.Interfaces.Repositories;
using MiBiblioteca.Application.Interfaces.Services;
using MiBiblioteca.Domain.Entities;

namespace MiBiblioteca.Application.Services
{
    // La logica de creacion de libros (chequeo de ISBN duplicado, armado de
    // la entidad) vivia antes en BooksController. La movemos aca para que
    // los controllers de esta API sigan todos el mismo patron: reciben la
    // request, delegan la logica a un servicio de Application, y traducen
    // el resultado (o la excepcion) a un IActionResult - lo mismo que ya
    // hacia AuthController con IAuthService.
    public class BookService : IBookService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBookMetadataProvider _bookMetadataProvider;
        private readonly IMapper _mapper;

        public BookService(IUnitOfWork unitOfWork, IBookMetadataProvider bookMetadataProvider, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _bookMetadataProvider = bookMetadataProvider;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BookResponseDto>> GetAllAsync()
        {
            var books = await _unitOfWork.Books.GetAllAsync();
            return _mapper.Map<IEnumerable<BookResponseDto>>(books);
        }

        public async Task<BookResponseDto?> GetByIdAsync(Guid id)
        {
            var book = await _unitOfWork.Books.GetByIdAsync(id);
            return book is null ? null : _mapper.Map<BookResponseDto>(book);
        }

        public async Task<BookResponseDto> CreateAsync(CreateBookDto dto)
        {
            await EnsureIsbnIsAvailableAsync(dto.Isbn);

            var book = _mapper.Map<Book>(dto);

            _unitOfWork.Books.Add(book);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<BookResponseDto>(book);
        }

        public async Task<BookResponseDto?> CreateFromIsbnAsync(string isbn)
        {
            await EnsureIsbnIsAvailableAsync(isbn);

            var metadata = await _bookMetadataProvider.GetByIsbnAsync(isbn);
            if (metadata is null)
            {
                return null;
            }

            var book = new Book
            {
                Isbn = isbn,
                Title = metadata.Title,
                Author = metadata.Author,
                CoverUrl = metadata.CoverUrl,
                PublishedYear = metadata.PublishedYear
            };

            _unitOfWork.Books.Add(book);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<BookResponseDto>(book);
        }

        private async Task EnsureIsbnIsAvailableAsync(string isbn)
        {
            var existing = await _unitOfWork.Books.GetByIsbnAsync(isbn);
            if (existing is not null)
            {
                throw new InvalidOperationException($"Ya existe un libro con el ISBN {isbn}.");
            }
        }
    }
}
