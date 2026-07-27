namespace MiBiblioteca.Application.Dto
{
    // Lo que la API devuelve al cliente. A diferencia de la entidad Book,
    // no incluye la lista de ReadingEntries (evita el problema de referencias
    // circulares Book -> ReadingEntry -> Book al serializar, y de paso es
    // el mismo motivo por el que en AutoMapper no conviene mapear tipos que
    // se referencian a si mismos sin limite - ver el comentario en
    // MappingProfile).
    public class BookResponseDto
    {
        public Guid Id { get; set; }
        public string Isbn { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string? CoverUrl { get; set; }
        public int? PublishedYear { get; set; }
        public DateTimeOffset DateCreated { get; set; }
    }
}
