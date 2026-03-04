using System.Reflection;
using Microsoft.AspNetCore.Authentication.Cookies;
using NorthwindPlatform.Bff.Passenger.Extensions.AspNet;
using NorthwindPlatform.Bff.Passenger.Extensions.Ioc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services
    .AddServices()
    .AddHttpClients(builder.Configuration);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:4200", "http://127.0.0.1:4200")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.Name = "Passenger.Portal";
        options.Cookie.HttpOnly = true;
        options.Cookie.SameSite = SameSiteMode.Strict;
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        options.LoginPath = "/Account/Login";
    });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("AllowLocalFrontend");
app.UseAuthentication(); 

app.UseHttpsRedirection();
app.MapEndpoints(Assembly.GetExecutingAssembly());
app.Run();
