using NorthwindPlatform.Staff.Service.Infrastructure.EventStore.Ioc;
using NorthwindPlatform.Staff.Service.Infrastructure.PostgreSQL.Ioc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services
    .AddEventStoreInfra(builder.Configuration)
    .AddPostgreSQLInfra(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.Run();