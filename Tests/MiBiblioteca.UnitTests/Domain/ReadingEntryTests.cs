using MiBiblioteca.Domain.Entities;
using MiBiblioteca.Domain.Enums;
using Xunit;

namespace MiBiblioteca.UnitTests.Domain
{
    // Ejercicio de TDD (Test Driven Development): la regla de negocio es
    // "no se puede marcar un libro como Read sin ponerle un puntaje de 1 a 5".
    //
    // El metodo ReadingEntry.MarkAsRead() todavia NO EXISTE cuando este
    // archivo se escribe. Ese es el punto: en TDD primero escribimos el
    // test (falla - "red"), despues escribimos el minimo codigo para que
    // pase (pasa - "green"), y recien ahi, con la seguridad de tener un
    // test que lo cubre, mejoramos el codigo si hace falta ("refactor").
    public class ReadingEntryTests
    {
        [Fact]
        public void MarkAsRead_ConRatingValido_CambiaEstadoYGuardaElPuntaje()
        {
            // Arrange
            var entry = new ReadingEntry { Status = ReadingStatus.Reading };

            // Act
            entry.MarkAsRead(5);

            // Assert
            Assert.Equal(ReadingStatus.Read, entry.Status);
            Assert.Equal(5, entry.Rating);
            Assert.NotNull(entry.DateFinished);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(6)]
        [InlineData(-1)]
        public void MarkAsRead_ConRatingFueraDeRango_LanzaExcepcion(int ratingInvalido)
        {
            // Arrange
            var entry = new ReadingEntry { Status = ReadingStatus.Reading };

            // Act + Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => entry.MarkAsRead(ratingInvalido));
        }
    }
}
