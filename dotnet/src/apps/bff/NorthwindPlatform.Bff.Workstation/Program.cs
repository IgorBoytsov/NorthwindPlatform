using System.Reflection;
using NorthwindPlatform.Bff.Workstation.Extensions.AspNet;
using NorthwindPlatform.Bff.Workstation.Extensions.Ioc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services
    .AddServices()
    .AddHttpClients(builder.Configuration)
    .AddPersistence(builder.Configuration);

var app = builder.Build();

app.MapEndpoints(Assembly.GetExecutingAssembly());

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.Run();