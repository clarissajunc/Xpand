using Microsoft.EntityFrameworkCore;
using Xpand.PlanetsAPI.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//TODO move the conStr to appsettings
builder.Services.AddControllers();
builder.Services.AddDbContext<PlanetContext>(opt => { opt.UseSqlServer(builder.Configuration.GetConnectionString("Database")); });

var app = builder.Build();

//TODO
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

