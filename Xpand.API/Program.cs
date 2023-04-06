using Microsoft.Extensions.Configuration;
using Xpand.API;
using Xpand.API.Managers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddScoped<IDashboardManager, DashboardManager>();
builder.Services.AddHttpClient<IDashboardManager, DashboardManager>();
builder.Services.Configure<ServicesConfiguration>(
    builder.Configuration.GetSection(ServicesConfiguration.Position)
);
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowXpandWebApp", builder =>
    {
        builder.WithOrigins("http://localhost:4200");
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseCors("AllowXpandWebApp");

app.UseAuthorization();

app.MapControllers();

app.Run();
