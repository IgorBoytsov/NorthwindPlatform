using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace NorthwindPlatform.Staff.Service.Infrastructure.PostgreSQL.Ioc
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPostgreSQLInfra(this IServiceCollection services, IConfiguration configuration)
        {
            return services;
        }
    }
}