using System.Linq.Expressions;

namespace MiBiblioteca.Application.Interfaces.Repositories
{
    // Repository generico. El material de la catedra lo muestra con "int id"
    // para GetId/GetIdAsync, pero como nuestras entidades usan Guid (ver
    // BaseEntity), generalizamos un paso mas agregando TId como segundo
    // parametro generico - la misma idea que menciona el apunte al final:
    // "podemos utilizar los Generic pasandoles parametros... como TId".
    public interface IRepositoryBase<TEntity, TId> where TEntity : class
    {
        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);

        TEntity? GetById(TId id);
        Task<TEntity?> GetByIdAsync(TId id);

        TEntity? Get(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate);

        IEnumerable<TEntity> GetList(Expression<Func<TEntity, bool>> predicate);
        Task<IEnumerable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate);

        IEnumerable<TEntity> GetAll();
        Task<IEnumerable<TEntity>> GetAllAsync();

        int Count();
        Task<int> CountAsync();

        void Update(TEntity entity);
        void Remove(TEntity entity);
    }
}
