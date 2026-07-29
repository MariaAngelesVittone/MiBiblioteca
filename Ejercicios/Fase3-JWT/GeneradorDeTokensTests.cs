using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Fase3Jwt
{
    public class GeneradorDeTokensTests
    {
        private const string ClaveSecreta = "una-clave-secreta-de-prueba-bien-larga-1234567890";

        [Fact]
        public void GenerarToken_DevuelveUnStringConTresPartes()
        {
            // Arrange + Act
            var token = GeneradorDeTokens.GenerarToken("ana", ClaveSecreta, 30);

            // Assert: un JWT tiene la forma header.payload.signature
            var partes = token.Split('.');
            Assert.Equal(3, partes.Length);
        }

        [Fact]
        public void ValidarYObtenerUsername_ConTokenValido_DevuelveElUsername()
        {
            // Arrange
            var token = GeneradorDeTokens.GenerarToken("ana", ClaveSecreta, 30);

            // Act
            var username = GeneradorDeTokens.ValidarYObtenerUsername(token, ClaveSecreta);

            // Assert
            Assert.Equal("ana", username);
        }

        [Fact]
        public void ValidarYObtenerUsername_ConClaveIncorrecta_DevuelveNull()
        {
            // Arrange
            var token = GeneradorDeTokens.GenerarToken("ana", ClaveSecreta, 30);

            // Act: alguien intenta validar el token con una clave que no
            // es la que uso el servidor para firmarlo.
            var username = GeneradorDeTokens.ValidarYObtenerUsername(token, "otra-clave-completamente-distinta-98765");

            // Assert
            Assert.Null(username);
        }

        [Fact]
        public void ValidarYObtenerUsername_ConTokenVencido_DevuelveNull()
        {
            // Arrange: armamos un token ya vencido a mano (JwtSecurityToken
            // no deja pedir "expires" antes de "notBefore", asi que no
            // podemos generarlo con GenerarToken y un numero negativo de
            // minutos). Lo alejamos 30/20 minutos en el pasado -no solo 1
            // o 2- porque la libreria de validacion tolera unos minutos de
            // diferencia de reloj entre servidores (clock skew) por
            // defecto.
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ClaveSecreta));
            var credenciales = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim> { new("username", "ana") };
            var tokenVencido = new JwtSecurityToken(
                claims: claims,
                notBefore: DateTime.UtcNow.AddMinutes(-30),
                expires: DateTime.UtcNow.AddMinutes(-20),
                signingCredentials: credenciales);
            var token = new JwtSecurityTokenHandler().WriteToken(tokenVencido);

            // Act
            var username = GeneradorDeTokens.ValidarYObtenerUsername(token, ClaveSecreta);

            // Assert
            Assert.Null(username);
        }

        [Fact]
        public void ValidarYObtenerUsername_ConTokenCorrupto_DevuelveNullSinLanzarExcepcion()
        {
            // Arrange: le cambiamos el final de la firma - un atacante
            // que intente modificar el token a mano termina en este caso.
            var token = GeneradorDeTokens.GenerarToken("ana", ClaveSecreta, 30);
            var tokenCorrupto = token[..^5] + "aaaaa";

            // Act
            var username = GeneradorDeTokens.ValidarYObtenerUsername(tokenCorrupto, ClaveSecreta);

            // Assert
            Assert.Null(username);
        }
    }
}
