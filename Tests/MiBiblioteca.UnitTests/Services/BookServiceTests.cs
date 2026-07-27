using AutoMapper;
using MiBiblioteca.Application.Dto;
using MiBiblioteca.Application.Interfaces.Repositories;
using MiBiblioteca.Application.Interfaces.Services;
using MiBiblioteca.Application.Services;
using MiBiblioteca.Domain.Entities;
using Moq;
using Xunit;

namespace MiBiblioteca.UnitTests.Services
{
    public class BookServiceTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IBookRepository> _bookRepositoryMock;
        private readonly Mock<IBookMetadataProvider> _metadataProviderMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly BookService _sut;

        public BookServiceTests()
        {
            _bookRepositoryMock = new Mock<IBookRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _unitOfWorkMock.Setup(u => u.Books).Returns(_bookRepositoryMock.Object);
            _metadataProviderMock = new Mock<IBookMetadataProvider>();
            _mapperMock = new Mock<IMapper>();

            _sut = new BookService(_unitOfWorkMock.Object, _metadataProviderMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task CreateAsync_ConIsbnExistente_LanzaExcepcion()
        {
            // Arrange
            var dto = new CreateBookDto { Isbn = "978-0-13-468599-1", Title = "Clean Code", Author = "Robert C. Martin" };
            _bookRepositoryMock.Setup(r => r.GetByIsbnAsync(dto.Isbn)).ReturnsAsync(new Book());

            // Act + Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => _sut.CreateAsync(dto));
            _unitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.Never);
        }

        [Fact]
        public async Task CreateAsync_ConIsbnNuevo_GuardaYDevuelveElLibroMapeado()
        {
            // Arrange
            var dto = new CreateBookDto { Isbn = "978-0-13-468599-1", Title = "Clean Code", Author = "Robert C. Martin" };
            _bookRepositoryMock.Setup(r => r.GetByIsbnAsync(dto.Isbn)).ReturnsAsync((Book?)null);

            var book = new Book { Isbn = dto.Isbn, Title = dto.Title, Author = dto.Author };
            var responseDto = new BookResponseDto { Isbn = dto.Isbn, Title = dto.Title, Author = dto.Author };
            _mapperMock.Setup(m => m.Map<Book>(dto)).Returns(book);
            _mapperMock.Setup(m => m.Map<BookResponseDto>(book)).Returns(responseDto);

            // Act
            var result = await _sut.CreateAsync(dto);

            // Assert
            Assert.Equal(responseDto, result);
            _bookRepositoryMock.Verify(r => r.Add(book), Times.Once);
            _unitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task CreateFromIsbnAsync_ConIsbnExistente_LanzaExcepcion()
        {
            // Arrange
            var isbn = "978-0-13-468599-1";
            _bookRepositoryMock.Setup(r => r.GetByIsbnAsync(isbn)).ReturnsAsync(new Book());

            // Act + Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => _sut.CreateFromIsbnAsync(isbn));
            _metadataProviderMock.Verify(m => m.GetByIsbnAsync(It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public async Task CreateFromIsbnAsync_SinMetadataEnOpenLibrary_DevuelveNull()
        {
            // Arrange
            var isbn = "000-0-00-000000-0";
            _bookRepositoryMock.Setup(r => r.GetByIsbnAsync(isbn)).ReturnsAsync((Book?)null);
            _metadataProviderMock.Setup(m => m.GetByIsbnAsync(isbn)).ReturnsAsync((BookMetadataDto?)null);

            // Act
            var result = await _sut.CreateFromIsbnAsync(isbn);

            // Assert
            Assert.Null(result);
            _unitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.Never);
        }
    }
}
