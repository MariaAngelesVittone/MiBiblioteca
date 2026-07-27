using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiBiblioteca.Application.Dto;
using MiBiblioteca.Application.Interfaces.Repositories;
using MiBiblioteca.Domain.Entities;

namespace MiBiblioteca.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public BooksController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var books = await _unitOfWork.Books.GetAllAsync();
            return Ok(books);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var book = await _unitOfWork.Books.GetByIdAsync(id);
            if (book is null) return NotFound();
            return Ok(book);
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

            var book = new Book
            {
                Isbn = dto.Isbn,
                Title = dto.Title,
                Author = dto.Author,
                CoverUrl = dto.CoverUrl,
                PublishedYear = dto.PublishedYear
            };

            _unitOfWork.Books.Add(book);
            await _unitOfWork.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = book.Id }, book);
        }
    }
}
