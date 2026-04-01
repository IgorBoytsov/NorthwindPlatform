using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace NorthwindPlatform.Staff.Service.Infrastructure.EventStore.Ioc
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddEventStoreInfra(this IServiceCollection services, IConfiguration configuration)
        {
            return services;
        }
    }
}