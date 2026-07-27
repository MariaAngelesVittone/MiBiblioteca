using MiBiblioteca.Domain.Entities;
using MiBiblioteca.Persistence.Context;
using MiBiblioteca.Persistence.Repository;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace MiBiblioteca.IntegrationTests.Repository
{
    // Un test de INTEGRACION prueba que varias piezas reales funcionen
    // juntas - en este caso, que BookRepository realmente sepa hablar
    // con una base de datos sqlite de verdad. A diferencia de los tests
    // unitarios (que mockean todo), aca no hay dobles de prueba: si el
    // LINQ que escribimos en RepositoryBase no se traduce correctamente
    // a SQL, este test lo va a detectar y uno unitario con mocks jamas
    // lo hubiera notado.
    //
    // Usamos un archivo sqlite temporal por clase de test (no ":memory:")
    // a proposito: la base en memoria de EF Core no es un motor SQL real
    // y puede ocultar bugs que solo aparecen contra sqlite/SQL Server de
    // verdad.
    public class BookRepositoryTests : IDisposable
    {
        private readonly string _dbPath;
        private readonly MiBibliotecaContext _context;
        private readonly BookRepository _sut;

        public BookRepositoryTests()
        {
            _dbPath = Path.Combine(Path.GetTempPath(), $"mibiblioteca-tests-{Guid.NewGuid()}.db");

            var options = new DbContextOptionsBuilder<MiBibliotecaContext>()
                .UseSqlite($"Data Source={_dbPath}")
                .Options;

            _context = new MiBibliotecaContext(options);
            _context.Database.EnsureCreated();

            _sut = new BookRepository(_context);
        }

        [Fact]
        public async Task Add_YSaveChanges_PersisteElLibroDeVerdad()
        {
            // Arrange
            var book = new Book { Isbn = "111", Title = "Domain-Driven Design", Author = "Eric Evans" };

            // Act
            _sut.Add(book);
            await _context.SaveChangesAsync();

            // Assert: abrimos un DbContext NUEVO contra el mismo archivo,
            // para asegurarnos de que estamos leyendo lo que quedo
            // guardado en disco y no solo lo que quedo en memoria.
            var options = new DbContextOptionsBuilder<MiBibliotecaContext>()
                .UseSqlite($"Data Source={_dbPath}")
                .Options;
            using var freshContext = new MiBibliotecaContext(options);
            var freshRepo = new BookRepository(freshContext);

            var found = await freshRepo.GetByIdAsync(book.Id);
            Assert.NotNull(found);
            Assert.Equal("Domain-Driven Design", found!.Title);
        }

        [Fact]
        public async Task GetByIsbnAsync_ConIsbnExistente_LoEncuentra()
        {
            // Arrange
            var book = new Book { Isbn = "9780134685991", Title = "Effective Java", Author = "Joshua Bloch" };
            _sut.Add(book);
            await _context.SaveChangesAsync();

            // Act
            var found = await _sut.GetByIsbnAsync("9780134685991");

            // Assert
            Assert.NotNull(found);
            Assert.Equal("Effective Java", found!.Title);
        }

        [Fact]
        public async Task GetByIsbnAsync_ConIsbnInexistente_DevuelveNull()
        {
            // Act
            var found = await _sut.GetByIsbnAsync("no-existe");

            // Assert
            Assert.Null(found);
        }

        [Fact]
        public async Task CountAsync_CuentaSoloLosLibrosGuardados()
        {
            // Arrange
            _sut.Add(new Book { Isbn = "1", Title = "Uno", Author = "A" });
            _sut.Add(new Book { Isbn = "2", Title = "Dos", Author = "B" });
            await _context.SaveChangesAsync();

            // Act
            var count = await _sut.CountAsync();

            // Assert
            Assert.Equal(2, count);
        }

        public void Dispose()
        {
            _context.Dispose();

            // Microsoft.Data.Sqlite mantiene un pool de conexiones nativas
            // abiertas para poder reutilizarlas rapido. Sin esto, el archivo
            // sigue "en uso" incluso despues de hacer Dispose del DbContext,
            // y File.Delete revienta con IOException.
            SqliteConnection.ClearAllPools();

            if (File.Exists(_dbPath))
            {
                File.Delete(_dbPath);
            }
        }
    }
}
