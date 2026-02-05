using System.Reflection;
using NorthwindPlatform.Bff.Workstation.Infrastructure.Clients;

namespace NorthwindPlatform.Bff.Workstation.Extensions.Ioc
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            return services;
        }

        public static IServiceCollection AddHttpClients(this IServiceCollection services, IConfiguration configuration)
        {
            var authBaseUrl = configuration["Urls:AuthServicesBase"];
            string authenticationServices = "AuthenticationServices";
            services.AddHttpClient<IAuthClient, AuthClient>(authenticationServices, client => client.BaseAddress = new Uri(authBaseUrl!));

            return services;
        }
    }
}