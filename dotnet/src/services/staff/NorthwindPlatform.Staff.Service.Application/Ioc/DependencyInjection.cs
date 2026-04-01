using Microsoft.Extensions.DependencyInjection;

namespace NorthwindPlatform.Staff.Service.Application.Ioc
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            return services;
        }
    }
}