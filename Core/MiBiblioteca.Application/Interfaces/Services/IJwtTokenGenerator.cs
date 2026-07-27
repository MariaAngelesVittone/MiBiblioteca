using MiBiblioteca.Domain.Entities;

namespace MiBiblioteca.Application.Interfaces.Services
{
    // Application define QUE necesita (generar un token para un usuario),
    // pero no COMO se genera - eso depende de una libreria concreta
    // (System.IdentityModel.Tokens.Jwt) y de configuracion (el secret del
    // appsettings.json). Por eso la implementacion vive en Infrastructure
    // (proyecto MiBiblioteca.Identity), no aca.
    public interface IJwtTokenGenerator
    {
        string GenerateToken(User user);
    }
}
