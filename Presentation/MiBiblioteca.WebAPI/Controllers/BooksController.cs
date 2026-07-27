using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiBiblioteca.Persistence.Context;

namespace MiBiblioteca.WebAPI.Controllers
{
    // Controller minimo solo para probar que las 4 capas estan conectadas
    // (Domain -> Application -> Persistence -> WebAPI). En la Fase 2 esto
    // se reemplaza por el Repository Generico, no se accede al DbContext
    // directo desde un controller.
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly MiBibliotecaContext _context;

        public BooksController(MiBibliotecaContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var books = await _context.Books.ToListAsync();
            return Ok(books);
        }
    }
}
