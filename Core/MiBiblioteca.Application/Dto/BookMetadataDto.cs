namespace MiBiblioteca.Application.Dto
{
    // Lo que traemos de una fuente externa (Open Library) para autocompletar
    // los datos de un libro a partir de su ISBN.
    public class BookMetadataDto
    {
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string? CoverUrl { get; set; }
        public int? PublishedYear { get; set; }
    }
}
