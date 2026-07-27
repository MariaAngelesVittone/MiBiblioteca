namespace MiBiblioteca.Identity
{
    // Representa la seccion "Jwt" del appsettings.json, para no andar
    // navegando IConfiguration como diccionario en todos lados.
    public class JwtSettings
    {
        public string SecretKey { get; set; } = string.Empty;
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
        public int ExpiryHours { get; set; } = 1;
    }
}
