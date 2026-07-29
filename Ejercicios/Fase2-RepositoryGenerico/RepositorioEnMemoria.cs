namespace Fase2RepositoryGenerico
{
    // ============================================================
    // EJERCICIO: Repository Generico
    // ============================================================
    // Esta es tu version simplificada -sin base de datos, todo en
    // memoria con un Dictionary- de RepositoryBase<TEntity, TId>
    // (Infrastructure/MiBiblioteca.Persistence/Repository/RepositoryBase.cs
    // en el proyecto principal). Alli en vez de un Dictionary hay un
    // DbSet<TEntity> de Entity Framework que habla con la base de datos de
    // verdad, pero el CONTRATO (que metodos tiene, que generico usa) es
    // identico. Esa es la idea central del patron: el CODIGO QUE USA el
    // repositorio (un controller, un servicio) no le importa si atras hay
    // un Dictionary en memoria o una base de datos real.
    //
    // No corras este proyecto directo: complet los metodos de abajo y
    // despues corré los tests con:
    //   cd Ejercicios/Fase2-RepositoryGenerico.Tests
    //   dotnet test
    // Vas a ver tests en rojo (fallando) hasta que completes cada metodo -
    // esto es TDD (Fase 4 del proyecto principal): los tests ya estan
    // escritos ANTES que el codigo, y tu objetivo es hacerlos pasar.
    public class RepositorioEnMemoria<TEntity, TId> : IRepositorio<TEntity, TId>
        where TEntity : IConId<TId>
        where TId : notnull
    {
        private readonly Dictionary<TId, TEntity> _datos = new();

        public void Agregar(TEntity entidad)
        {
            // TODO: completar
            // Pista: _datos[entidad.Id] = entidad;
            throw new NotImplementedException();
        }

        public TEntity? BuscarPorId(TId id)
        {
            // TODO: completar
            // Pista: _datos.TryGetValue(id, out var entidad) devuelve
            // true/false, y te da la entidad encontrada (o el valor por
            // defecto) en el parametro "out".
            throw new NotImplementedException();
        }

        public List<TEntity> ObtenerTodos()
        {
            // TODO: completar
            // Pista: _datos.Values es la coleccion de valores del
            // diccionario (sin las claves) - convertila a List con
            // .ToList() (usando System.Linq, ya disponible).
            throw new NotImplementedException();
        }

        public void Eliminar(TId id)
        {
            // TODO: completar
            // Pista: _datos.Remove(id);
            throw new NotImplementedException();
        }

        public int Contar()
        {
            // TODO: completar
            throw new NotImplementedException();
        }
    }
}
