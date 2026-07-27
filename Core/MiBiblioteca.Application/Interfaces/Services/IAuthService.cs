using MiBiblioteca.Application.Dto;

namespace MiBiblioteca.Application.Interfaces.Services
{
    public interface IAuthService
    {
        Task<UserResponseDto> RegisterAsync(RegisterUserDto dto);

        // Devuelve el JWT si las credenciales son validas, null si no.
        Task<string?> LoginAsync(LoginUserDto dto);
    }
}
