using Microsoft.EntityFrameworkCore;

namespace Shared.Application.Data
{
    public interface IBaseWriteDbContext
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
    }
}