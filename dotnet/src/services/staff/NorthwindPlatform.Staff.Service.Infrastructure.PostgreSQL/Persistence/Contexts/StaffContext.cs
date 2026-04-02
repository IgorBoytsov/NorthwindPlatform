using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace NorthwindPlatform.Staff.Service.Infrastructure.PostgreSQL.Persistence.Contexts
{
    public sealed class StaffContext(DbContextOptions<StaffContext> options) : DbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}