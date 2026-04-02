using EventStore.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NorthwindPlatform.Staff.Service.Application.Abstractions.Repositories;
using NorthwindPlatform.Staff.Service.Infrastructure.EventStore.Events;
using NorthwindPlatform.Staff.Service.Infrastructure.EventStore.Repositories;

namespace NorthwindPlatform.Staff.Service.Infrastructure.EventStore.Ioc
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddEventStoreInfra(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration["ConnectionStrings:EventStore"];

            if (string.IsNullOrWhiteSpace(connectionString))
                throw new InvalidOperationException("EventStore connection string is missing!");

            var settings = EventStoreClientSettings.Create(connectionString);
            services.AddSingleton(new EventStoreClient(settings));
            services.AddScoped<IEventStoreRepository, EventStoreRepository>();

            services.AddSingleton<EventTypeRegistry>();
            services.AddSingleton<IEventMapper, EventMapper>();

            return services;
        }
    }
}