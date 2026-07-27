using MiBiblioteca.Domain.Common;
using MiBiblioteca.Domain.Enums;

namespace MiBiblioteca.Domain.Entities
{
    // La relacion entre un usuario y un libro: en que estado esta su lectura,
    // que puntaje le puso y su comentario.
    public class ReadingEntry : BaseEntity
    {
        public Guid UserId { get; set; }
        public User? User { get; set; }

        public Guid BookId { get; set; }
        public Book? Book { get; set; }

        public ReadingStatus Status { get; set; } = ReadingStatus.WantToRead;
        public int? Rating { get; set; }
        public string? Comment { get; set; }
        public DateTimeOffset? DateStarted { get; set; }
        public DateTimeOffset? DateFinished { get; set; }
    }
}
