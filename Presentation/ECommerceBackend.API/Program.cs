using ECommerceBackend.Application;
using ECommerceBackend.Persistence;
using ECommerceBackend.Persistence.Contexts;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddPersistenceServices();
builder.Services.AddApplicationServices();

builder.Services.AddCors(options => options.AddDefaultPolicy(policy =>
    policy.WithOrigins("http://localhost:4200", "https://localhost:4200").AllowAnyHeader().AllowAnyMethod()
));

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseCors();

app.UseAuthorization();

app.MapControllers();

try
{
  using var scope = app.Services.CreateScope();
  var services = scope.ServiceProvider;
  var context = services.GetRequiredService<ECommerceBackendDbContext>();
  await context.Database.MigrateAsync();
  await StoreContextSeed.SeedAsync(context);
}
catch (Exception ex)
{
  Console.WriteLine(ex);
  throw;
}

app.Run();
