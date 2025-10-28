namespace Shared.Application.Data
{
    public interface IBaseReadDbContext
    {
        IQueryable<TEntity> Set<TEntity>() where TEntity : class;
    }
}