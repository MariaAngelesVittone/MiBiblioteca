using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiBiblioteca.Application.Dto;
using MiBiblioteca.Application.Interfaces.Services;

namespace MiBiblioteca.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _bookService.GetAllAsync());
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var book = await _bookService.GetByIdAsync(id);
            if (book is null) return NotFound();
            return Ok(book);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreateBookDto dto)
        {
            try
            {
                var book = await _bookService.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = book.Id }, book);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // En vez de tipear titulo/autor/tapa a mano, los buscamos en Open
        // Library a partir del ISBN. Esta llamada pasa por HttpClientFactory
        // con politicas de Retry + Circuit Breaker (ver ExternalServices).
        [HttpPost("from-isbn/{isbn}")]
        [Authorize]
        public async Task<IActionResult> CreateFromIsbn(string isbn)
        {
            try
            {
                var book = await _bookService.CreateFromIsbnAsync(isbn);
                if (book is null)
                {
                    return NotFound($"No se encontro informacion para el ISBN {isbn} en Open Library.");
                }

                return CreatedAtAction(nameof(GetById), new { id = book.Id }, book);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
