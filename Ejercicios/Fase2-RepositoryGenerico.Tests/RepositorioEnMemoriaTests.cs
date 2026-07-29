using Fase2RepositoryGenerico;
using Xunit;

namespace Fase2RepositoryGenerico.Tests
{
    // Entidades de prueba chicas, solo para estos tests. Notar que Autor
    // usa Guid como Id (igual que las entidades reales de MiBiblioteca) y
    // Libro usa int - el ultimo test de abajo prueba que
    // RepositorioEnMemoria funciona con los dos, porque es REALMENTE
    // generico.
    public class Autor : IConId<Guid>
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Nombre { get; set; } = string.Empty;
    }

    public class Libro : IConId<int>
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
    }

    public class RepositorioEnMemoriaTests
    {
        [Fact]
        public void Agregar_YBuscarPorId_EncuentraLaEntidadAgregada()
        {
            // Arrange
            var repositorio = new RepositorioEnMemoria<Autor, Guid>();
            var autor = new Autor { Nombre = "Jorge Luis Borges" };

            // Act
            repositorio.Agregar(autor);
            var encontrado = repositorio.BuscarPorId(autor.Id);

            // Assert
            Assert.NotNull(encontrado);
            Assert.Equal("Jorge Luis Borges", encontrado!.Nombre);
        }

        [Fact]
        public void BuscarPorId_ConIdInexistente_DevuelveNull()
        {
            // Arrange
            var repositorio = new RepositorioEnMemoria<Autor, Guid>();

            // Act
            var resultado = repositorio.BuscarPorId(Guid.NewGuid());

            // Assert
            Assert.Null(resultado);
        }

        [Fact]
        public void ObtenerTodos_DevuelveTodasLasEntidadesAgregadas()
        {
            // Arrange
            var repositorio = new RepositorioEnMemoria<Autor, Guid>();
            repositorio.Agregar(new Autor { Nombre = "Silvina Ocampo" });
            repositorio.Agregar(new Autor { Nombre = "Adolfo Bioy Casares" });

            // Act
            var todos = repositorio.ObtenerTodos();

            // Assert
            Assert.Equal(2, todos.Count);
        }

        [Fact]
        public void Eliminar_SacaLaEntidadDelRepositorio()
        {
            // Arrange
            var repositorio = new RepositorioEnMemoria<Autor, Guid>();
            var autor = new Autor { Nombre = "Julio Cortazar" };
            repositorio.Agregar(autor);

            // Act
            repositorio.Eliminar(autor.Id);

            // Assert
            Assert.Null(repositorio.BuscarPorId(autor.Id));
        }

        [Fact]
        public void Contar_DevuelveLaCantidadDeEntidadesAgregadas()
        {
            // Arrange
            var repositorio = new RepositorioEnMemoria<Autor, Guid>();
            Assert.Equal(0, repositorio.Contar());

            // Act
            repositorio.Agregar(new Autor { Nombre = "Cesar Aira" });

            // Assert
            Assert.Equal(1, repositorio.Contar());
        }

        [Fact]
        public void FuncionaIgualConOtroTipoDeEntidadYOtroTipoDeId()
        {
            // Este test prueba que el repositorio es REALMENTE generico:
            // sirve para Libro con Id int, no solo para Autor con Id Guid.
            var repositorio = new RepositorioEnMemoria<Libro, int>();
            var libro = new Libro { Id = 1, Titulo = "Ficciones" };

            repositorio.Agregar(libro);

            Assert.Equal("Ficciones", repositorio.BuscarPorId(1)!.Titulo);
        }
    }
}
