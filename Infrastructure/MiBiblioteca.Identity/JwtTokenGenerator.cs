using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MiBiblioteca.Application.Interfaces.Services;
using MiBiblioteca.Domain.Entities;

namespace MiBiblioteca.Identity
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly JwtSettings _settings;

        public JwtTokenGenerator(IOptions<JwtSettings> options)
        {
            _settings = options.Value;
        }

        public string GenerateToken(User user)
        {
            // 1. La clave secreta que solo conoce el servidor (el "Secret" del apunte).
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.SecretKey));

            // 2. Las credenciales de firma: la clave + el algoritmo de hashing.
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // 3. Las claims que van en el payload. "sub" es el identificador
            // estandar para el id del usuario, es lo primero que va a buscar
            // cualquiera que lea el token.
            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new("username", user.Username),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            // 4. Armamos el JWT: emisor, audiencia, claims, fecha de creacion,
            // fecha de expiracion, y las credenciales de firma que definimos arriba.
            var token = new JwtSecurityToken(
                issuer: _settings.Issuer,
                audience: _settings.Audience,
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddHours(_settings.ExpiryHours),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
