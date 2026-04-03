using NorthwindPlatform.Staff.Service.Application.Ioc;
using NorthwindPlatform.Staff.Service.Infrastructure.KurrentDB.Ioc;
using NorthwindPlatform.Staff.Service.Infrastructure.PostgreSQL.Ioc;
using NorthwindPlatform.Staff.Service.Infrastructure.Projections.Ioc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services
    .AddApplication()
    .AddProjectionInfra()
    .AddEventStoreInfra(builder.Configuration)
    .AddPostgreSQLInfra(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();