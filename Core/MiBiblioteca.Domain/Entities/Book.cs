using MiBiblioteca.Domain.Common;

namespace MiBiblioteca.Domain.Entities
{
    public class Book : BaseEntity
    {
        public string Isbn { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string? CoverUrl { get; set; }
        public int? PublishedYear { get; set; }

        public List<ReadingEntry> ReadingEntries { get; set; } = new();
    }
}
