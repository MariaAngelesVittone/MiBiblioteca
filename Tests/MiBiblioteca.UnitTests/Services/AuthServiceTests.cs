using MiBiblioteca.Application.Dto;
using MiBiblioteca.Application.Interfaces.Repositories;
using MiBiblioteca.Application.Interfaces.Services;
using MiBiblioteca.Application.Services;
using MiBiblioteca.Domain.Entities;
using Moq;
using Xunit;

namespace MiBiblioteca.UnitTests.Services
{
    // Un test UNITARIO prueba una sola clase de forma aislada. AuthService
    // depende de IUnitOfWork y de IJwtTokenGenerator, pero acá no usamos
    // ninguna base de datos ni ninguna libreria de JWT real: los "mockeamos"
    // (los reemplazamos por dobles de prueba con Moq) para poder controlar
    // exactamente que devuelven y verificar solo la logica de AuthService.
    //
    // Si quisieramos probar que el repositorio realmente guarda en una base
    // de datos de verdad, eso ya no seria un test unitario sino de
    // INTEGRACION (ver MiBiblioteca.IntegrationTests).
    public class AuthServiceTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IJwtTokenGenerator> _tokenGeneratorMock;
        private readonly AuthService _sut; // "sut" = System Under Test

        public AuthServiceTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _unitOfWorkMock.Setup(u => u.Users).Returns(_userRepositoryMock.Object);
            _tokenGeneratorMock = new Mock<IJwtTokenGenerator>();

            _sut = new AuthService(_unitOfWorkMock.Object, _tokenGeneratorMock.Object);
        }

        [Fact]
        public async Task LoginAsync_ConCredencialesValidas_DevuelveToken()
        {
            // Arrange: preparamos los datos y el comportamiento de los mocks.
            var password = "Password123";
            var user = new User
            {
                Username = "lector1",
                Email = "lector1@test.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(password)
            };

            _userRepositoryMock
                .Setup(r => r.GetByUsernameAsync("lector1"))
                .ReturnsAsync(user);

            _tokenGeneratorMock
                .Setup(t => t.GenerateToken(user))
                .Returns("token-de-prueba");

            var dto = new LoginUserDto { Username = "lector1", Password = password };

            // Act: ejecutamos el metodo que queremos probar.
            var result = await _sut.LoginAsync(dto);

            // Assert: verificamos que el resultado sea el esperado.
            Assert.Equal("token-de-prueba", result);
        }

        [Fact]
        public async Task LoginAsync_ConUsuarioInexistente_DevuelveNull()
        {
            // Arrange
            _userRepositoryMock
                .Setup(r => r.GetByUsernameAsync(It.IsAny<string>()))
                .ReturnsAsync((User?)null);

            var dto = new LoginUserDto { Username = "no-existe", Password = "cualquiera" };

            // Act
            var result = await _sut.LoginAsync(dto);

            // Assert
            Assert.Null(result);
            // Ademas de verificar el resultado, podemos verificar que nunca
            // se intento generar un token si el usuario no existe.
            _tokenGeneratorMock.Verify(t => t.GenerateToken(It.IsAny<User>()), Times.Never);
        }

        [Fact]
        public async Task LoginAsync_ConPasswordIncorrecta_DevuelveNull()
        {
            // Arrange
            var user = new User
            {
                Username = "lector1",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("LaPasswordReal123")
            };
            _userRepositoryMock.Setup(r => r.GetByUsernameAsync("lector1")).ReturnsAsync(user);

            var dto = new LoginUserDto { Username = "lector1", Password = "otra-password" };

            // Act
            var result = await _sut.LoginAsync(dto);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task RegisterAsync_ConUsernameExistente_LanzaExcepcion()
        {
            // Arrange
            var existingUser = new User { Username = "lector1" };
            _userRepositoryMock.Setup(r => r.GetByUsernameAsync("lector1")).ReturnsAsync(existingUser);

            var dto = new RegisterUserDto { Username = "lector1", Email = "x@x.com", Password = "Password123" };

            // Act + Assert: cuando lo que queremos comprobar es que se lanza
            // una excepcion, Act y Assert quedan juntos en la misma linea.
            await Assert.ThrowsAsync<InvalidOperationException>(() => _sut.RegisterAsync(dto));
        }
    }
}
