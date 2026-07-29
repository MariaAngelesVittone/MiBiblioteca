using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Fase3Jwt
{
    // ============================================================
    // EJERCICIO: JWT (JSON Web Tokens)
    // ============================================================
    // Un JWT es un string con 3 partes separadas por puntos
    // (header.payload.signature) que prueba una identidad SIN que el
    // servidor tenga que guardar sesiones: toda la informacion necesaria
    // (quien es el usuario, cuando expira) viaja adentro del propio
    // token, y la FIRMA (la tercera parte) garantiza que nadie lo
    // modifico despues de que el servidor lo emitio - si alguien cambia
    // una sola letra del payload, la firma deja de coincidir con el
    // contenido y la validacion falla.
    //
    // Esta es una version simplificada de
    // Infrastructure/MiBiblioteca.Identity/JwtTokenGenerator.cs del
    // proyecto principal: misma libreria
    // (System.IdentityModel.Tokens.Jwt), mismas clases
    // (SymmetricSecurityKey, SigningCredentials, JwtSecurityToken), pero
    // con una sola claim ("username") en vez de las tres que usa
    // MiBiblioteca real, y sin Issuer/Audience para simplificar. Cuando
    // termines este ejercicio, anda a leer el archivo real y vas a
    // reconocer cada linea.
    public static class GeneradorDeTokens
    {
        // Debe generar un JWT que:
        //  - tenga una claim de tipo "username" con el valor de "username"
        //  - este firmado con HMAC-SHA256 (SecurityAlgorithms.HmacSha256)
        //    usando "claveSecreta" como clave simetrica
        //  - expire "minutosDeValidez" minutos despues de este momento
        //
        // Pista - los pasos son siempre estos 4:
        //   1. var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(claveSecreta));
        //   2. var credenciales = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        //   3. var claims = new List<Claim> { new("username", username) };
        //      var token = new JwtSecurityToken(
        //          claims: claims,
        //          notBefore: DateTime.UtcNow,
        //          expires: DateTime.UtcNow.AddMinutes(minutosDeValidez),
        //          signingCredentials: credenciales);
        //   4. return new JwtSecurityTokenHandler().WriteToken(token);
        public static string GenerarToken(string username, string claveSecreta, int minutosDeValidez)
        {
            // TODO: completar
            throw new NotImplementedException();
        }

        // Debe validar el token contra "claveSecreta" y devolver el valor
        // de la claim "username" si el token es valido. Si el token esta
        // vencido, fue firmado con otra clave, o esta corrupto de
        // cualquier otra forma, debe devolver null - NO debe dejar que la
        // excepcion que tira la libreria (SecurityTokenException y sus
        // variantes) se propague. Este es el mismo espiritu que
        // ApiExceptionFilter (Fase 9): un dato invalido no debe tirar
        // abajo el programa, se convierte en un resultado manejable
        // (null, en este caso).
        //
        // Pista:
        //   var handler = new JwtSecurityTokenHandler();
        //   var parametros = new TokenValidationParameters
        //   {
        //       ValidateIssuerSigningKey = true,
        //       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(claveSecreta)),
        //       ValidateIssuer = false,
        //       ValidateAudience = false,
        //   };
        //   try
        //   {
        //       var principal = handler.ValidateToken(token, parametros, out _);
        //       return principal.FindFirst("username")?.Value;
        //   }
        //   catch (SecurityTokenException)
        //   {
        //       return null;
        //   }
        public static string? ValidarYObtenerUsername(string token, string claveSecreta)
        {
            // TODO: completar
            throw new NotImplementedException();
        }
    }
}
