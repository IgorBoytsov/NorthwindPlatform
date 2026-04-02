using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace NorthwindPlatform.Staff.Service.Application.Ioc
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            var currentAssembly = Assembly.GetExecutingAssembly();
            services.AddMediatR(config => config.RegisterServicesFromAssembly(currentAssembly));
            services.AddValidatorsFromAssembly(currentAssembly);

            return services;
        }
    }
}