using Microsoft.OpenApi.Models;
using Xpand.API;
using Xpand.API.Managers;
using Xpand.API.Managers.Abstractions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo { Title = "Xpand API", Version = "v1" }));

builder.Services.AddScoped<ICrewManager, CrewManager>();
builder.Services.AddHttpClient<ICrewManager, CrewManager>();

builder.Services.AddScoped<IPlanetManager, PlanetManager>();
builder.Services.AddHttpClient<IPlanetManager, PlanetManager>();

builder.Services.Configure<ServicesConfiguration>(
    builder.Configuration.GetSection(ServicesConfiguration.Position)
);
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowXpandWebApp", builder =>
    {
        builder.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseExceptionHandler("/error");

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
