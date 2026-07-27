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

        // Regla de negocio: no se puede marcar un libro como leido sin
        // ponerle un puntaje de 1 a 5. Vive en la entidad (Domain) y no en
        // un Service, porque es una invariante propia de un ReadingEntry,
        // no una orquestacion de varias piezas.
        public void MarkAsRead(int rating)
        {
            if (rating is < 1 or > 5)
            {
                throw new ArgumentOutOfRangeException(nameof(rating), "El puntaje debe estar entre 1 y 5.");
            }

            Status = ReadingStatus.Read;
            Rating = rating;
            DateFinished = DateTimeOffset.UtcNow;
        }
    }
}
