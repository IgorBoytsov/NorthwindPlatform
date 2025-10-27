using Microsoft.EntityFrameworkCore;
using Shared.Application.Data;

namespace Shared.Persistence
{
    public class UnitOfWork<TContext>(TContext context) : IUnitOfWork
        where TContext : DbContext, IBaseWriteDbContext
    {
        private readonly TContext _context = context;

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
            => _context.SaveChangesAsync(cancellationToken);
    }
}