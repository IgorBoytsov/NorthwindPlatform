using Microsoft.Extensions.DependencyInjection;
using NorthwindPlatform.Staff.Service.Infrastructure.Projections.Services;

namespace NorthwindPlatform.Staff.Service.Infrastructure.Projections.Ioc
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddProjectionInfra(this IServiceCollection services)
        {
            services.AddHostedService<EventStoreProjectionService>();

            return services;
        }
    }
}