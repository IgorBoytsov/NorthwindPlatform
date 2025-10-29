using Common.Core.Primitives;
using Shared.Kernel.Primitives;

namespace Shared.Domain.Repositories
{
    public interface IBaseRepository<TEntity> where TEntity : class, IAggregateRoot
    {
        Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task<Maybe<TEntity>> GetByIdAsync(object id, CancellationToken cancellationToken = default);
        void Remove(TEntity entity);
    }
}