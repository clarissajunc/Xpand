using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using Xpand.API;
using Xpand.API.Managers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo { Title = "Xpand API", Version = "v1" }));

builder.Services.AddScoped<IDashboardManager, DashboardManager>();
builder.Services.AddHttpClient<IDashboardManager, DashboardManager>();
builder.Services.Configure<ServicesConfiguration>(
    builder.Configuration.GetSection(ServicesConfiguration.Position)
);
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowXpandWebApp", builder =>
    {
        builder.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Xpand API V1"));
}
// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseCors("AllowXpandWebApp");

app.UseAuthorization();

app.MapControllers();

app.Run();
