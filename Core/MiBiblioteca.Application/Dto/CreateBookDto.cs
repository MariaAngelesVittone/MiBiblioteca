namespace MiBiblioteca.Application.Dto
{
    // DTO de entrada: lo que el cliente puede mandar para crear un libro.
    // No exponemos la entidad Book directa como parametro del endpoint
    // porque el cliente no deberia poder mandar, por ejemplo, el Id o
    // IsDeleted - esos los maneja el propio sistema.
    public class CreateBookDto
    {
        public string Isbn { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string? CoverUrl { get; set; }
        public int? PublishedYear { get; set; }
    }
}
