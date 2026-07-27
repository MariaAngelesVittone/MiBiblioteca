using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiBiblioteca.Application.Dto;
using MiBiblioteca.Application.Interfaces.Repositories;
using MiBiblioteca.Application.Interfaces.Services;
using MiBiblioteca.Domain.Entities;

namespace MiBiblioteca.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBookMetadataProvider _bookMetadataProvider;
        private readonly IMapper _mapper;

        public BooksController(IUnitOfWork unitOfWork, IBookMetadataProvider bookMetadataProvider, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _bookMetadataProvider = bookMetadataProvider;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var books = await _unitOfWork.Books.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<BookResponseDto>>(books));
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var book = await _unitOfWork.Books.GetByIdAsync(id);
            if (book is null) return NotFound();
            return Ok(_mapper.Map<BookResponseDto>(book));
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreateBookDto dto)
        {
            var existing = await _unitOfWork.Books.GetByIsbnAsync(dto.Isbn);
            if (existing is not null)
            {
                return BadRequest($"Ya existe un libro con el ISBN {dto.Isbn}.");
            }

            var book = _mapper.Map<Book>(dto);

            _unitOfWork.Books.Add(book);
            await _unitOfWork.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = book.Id }, _mapper.Map<BookResponseDto>(book));
        }

        // En vez de tipear titulo/autor/tapa a mano, los buscamos en Open
        // Library a partir del ISBN. Esta llamada pasa por HttpClientFactory
        // con politicas de Retry + Circuit Breaker (ver ExternalServices).
        [HttpPost("from-isbn/{isbn}")]
        [Authorize]
        public async Task<IActionResult> CreateFromIsbn(string isbn)
        {
            var existing = await _unitOfWork.Books.GetByIsbnAsync(isbn);
            if (existing is not null)
            {
                return BadRequest($"Ya existe un libro con el ISBN {isbn}.");
            }

            var metadata = await _bookMetadataProvider.GetByIsbnAsync(isbn);
            if (metadata is null)
            {
                return NotFound($"No se encontro informacion para el ISBN {isbn} en Open Library.");
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

            return CreatedAtAction(nameof(GetById), new { id = book.Id }, _mapper.Map<BookResponseDto>(book));
        }
    }
}
