using System.Data;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using NorthwindPlatform.Bff.Workstation.Infrastructure.Clients;
using NorthwindPlatform.Bff.Workstation.Infrastructure.Persistence.Contexts;
using NorthwindPlatform.Bff.Workstation.Infrastructure.Repositories;
using Npgsql;

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

        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<WorkstationDbContext>(options =>
                options.UseNpgsql(
                    configuration.GetConnectionString("DefaultConnection"),
                    npgsql => npgsql.EnableRetryOnFailure()));
            
            services.AddSingleton<IDbConnection>(sp =>
            {
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                return new NpgsqlConnection(connectionString);
            });

            services.AddScoped<ITrustedDeviceRepository, TrustedDeviceRepository>();

            return services;
        }
    }
}