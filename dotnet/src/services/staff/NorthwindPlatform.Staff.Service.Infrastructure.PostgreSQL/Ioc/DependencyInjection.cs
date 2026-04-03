using System.Data;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NorthwindPlatform.Staff.Service.Application.Abstractions.Repositories;
using NorthwindPlatform.Staff.Service.Infrastructure.PostgreSQL.Dapper.TypeHandlers;
using NorthwindPlatform.Staff.Service.Infrastructure.PostgreSQL.Persistence.Contexts;
using NorthwindPlatform.Staff.Service.Infrastructure.PostgreSQL.Repositories.Employee;
using Npgsql;

namespace NorthwindPlatform.Staff.Service.Infrastructure.PostgreSQL.Ioc
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPostgreSQLInfra(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration["ConnectionStrings:PostgreSQL"];

            SqlMapper.AddTypeHandler(new DateOnlyTypeHandler());
            SqlMapper.AddTypeHandler(new TimeOnlyTypeHandler());

            services.AddDbContext<StaffContext>(options => options.UseNpgsql(connectionString));
            services.AddSingleton<IDbConnection>(sp => new NpgsqlConnection(connectionString));

            services.AddScoped<IEmployeeReadRepository, EmployeeReadRepository>();

            return services;
        }
    }
}