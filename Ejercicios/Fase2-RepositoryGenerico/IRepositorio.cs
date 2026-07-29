namespace Fase2RepositoryGenerico
{
    // Cualquier entidad que quiera guardarse en un IRepositorio tiene que
    // poder decir cual es su Id. BaseEntity en MiBiblioteca
    // (Core/MiBiblioteca.Domain/Common/BaseEntity.cs) cumple este mismo
    // rol con "public Guid Id { get; set; }".
    public interface IConId<TId>
    {
        TId Id { get; }
    }

    // Version simplificada, sin base de datos, de
    // IRepositoryBase<TEntity, TId>
    // (Core/MiBiblioteca.Application/Interfaces/Repositories/IRepositoryBase.cs
    // en el proyecto principal). Alli TId existe porque las entidades reales
    // usan Guid en vez de int - aca hacemos lo mismo a proposito, y el
    // ultimo test de RepositorioEnMemoriaTests prueba que tu implementacion
    // funciona con CUALQUIER TId, no solo con Guid.
    public interface IRepositorio<TEntity, TId> where TEntity : IConId<TId>
    {
        void Agregar(TEntity entidad);
        TEntity? BuscarPorId(TId id);
        List<TEntity> ObtenerTodos();
        void Eliminar(TId id);
        int Contar();
    }
}
