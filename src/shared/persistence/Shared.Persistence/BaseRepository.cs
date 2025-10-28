using Common.Core.Primitives;
using Microsoft.EntityFrameworkCore;
using Shared.Application.Data;
using Shared.Domain.Repositories;
using Shared.Kernel.Primitives;

namespace Shared.Persistence
{
    public abstract class BaseRepository<TEntity, TContext> : IBaseRepository<TEntity>
        where TEntity : class, IAggregateRoot
        where TContext : IBaseWriteDbContext
    {
        protected readonly TContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        protected BaseRepository(TContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public virtual async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            await _dbSet.AddAsync(entity, cancellationToken);
            return entity;
        }

        public virtual async Task<Maybe<TEntity>> GetByIdAsync(object id, CancellationToken cancellationToken = default) => await _dbSet.FindAsync([id], cancellationToken);

        public virtual void Remove(TEntity entity) => _dbSet.Remove(entity);
    }
}