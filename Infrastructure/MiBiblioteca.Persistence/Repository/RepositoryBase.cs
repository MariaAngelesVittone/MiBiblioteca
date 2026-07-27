using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MiBiblioteca.Application.Interfaces.Repositories;
using MiBiblioteca.Persistence.Context;

namespace MiBiblioteca.Persistence.Repository
{
    public class RepositoryBase<TEntity, TId> : IRepositoryBase<TEntity, TId> where TEntity : class
    {
        protected readonly MiBibliotecaContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        public RepositoryBase(MiBibliotecaContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public void Add(TEntity entity) => _dbSet.Add(entity);

        public void AddRange(IEnumerable<TEntity> entities) => _dbSet.AddRange(entities);

        public TEntity? GetById(TId id) => _dbSet.Find(id);

        public async Task<TEntity?> GetByIdAsync(TId id) => await _dbSet.FindAsync(id);

        public TEntity? Get(Expression<Func<TEntity, bool>> predicate) =>
            _dbSet.FirstOrDefault(predicate);

        public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate) =>
            await _dbSet.FirstOrDefaultAsync(predicate);

        public IEnumerable<TEntity> GetList(Expression<Func<TEntity, bool>> predicate) =>
            _dbSet.Where(predicate).ToList();

        public async Task<IEnumerable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate) =>
            await _dbSet.Where(predicate).ToListAsync();

        public IEnumerable<TEntity> GetAll() => _dbSet.ToList();

        public async Task<IEnumerable<TEntity>> GetAllAsync() => await _dbSet.ToListAsync();

        public int Count() => _dbSet.Count();

        public async Task<int> CountAsync() => await _dbSet.CountAsync();

        public void Update(TEntity entity) => _dbSet.Update(entity);

        public void Remove(TEntity entity) => _dbSet.Remove(entity);
    }
}
