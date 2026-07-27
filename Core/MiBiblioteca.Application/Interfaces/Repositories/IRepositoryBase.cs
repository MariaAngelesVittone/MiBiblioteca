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

        // Las versiones sync de abajo (GetById, Get, GetList, GetAll, Count)
        // quedan solo para completar el patron generico que muestra la
        // catedra. En este proyecto NINGUN controller ni servicio las usa:
        // EF Core es async de punta a punta, y llamarlas de verdad en un
        // endpoint bloquearia un thread del pool esperando I/O de base de
        // datos en vez de liberarlo mientras espera - con suficiente carga
        // concurrente, eso agota el thread pool. Regla practica: en una API
        // ASP.NET Core, siempre usar la variante *Async.
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
