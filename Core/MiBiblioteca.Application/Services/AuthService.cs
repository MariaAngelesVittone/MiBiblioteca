using MiBiblioteca.Application.Dto;
using MiBiblioteca.Application.Interfaces.Repositories;
using MiBiblioteca.Application.Interfaces.Services;
using MiBiblioteca.Domain.Entities;

namespace MiBiblioteca.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtTokenGenerator _tokenGenerator;

        public AuthService(IUnitOfWork unitOfWork, IJwtTokenGenerator tokenGenerator)
        {
            _unitOfWork = unitOfWork;
            _tokenGenerator = tokenGenerator;
        }

        public async Task<UserResponseDto> RegisterAsync(RegisterUserDto dto)
        {
            var existing = await _unitOfWork.Users.GetByUsernameAsync(dto.Username);
            if (existing is not null)
            {
                throw new InvalidOperationException("Ese nombre de usuario ya existe.");
            }

            var user = new User
            {
                Username = dto.Username,
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password)
            };

            _unitOfWork.Users.Add(user);
            await _unitOfWork.SaveChangesAsync();

            return new UserResponseDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email
            };
        }

        public async Task<string?> LoginAsync(LoginUserDto dto)
        {
            // Paso 2 del proceso de autenticacion: validar que el usuario
            // exista y que la contraseña le corresponda.
            var user = await _unitOfWork.Users.GetByUsernameAsync(dto.Username);
            if (user is null) return null;

            var passwordIsValid = BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash);
            if (!passwordIsValid) return null;

            // Paso 3: generar y devolver el token.
            return _tokenGenerator.GenerateToken(user);
        }
    }
}
