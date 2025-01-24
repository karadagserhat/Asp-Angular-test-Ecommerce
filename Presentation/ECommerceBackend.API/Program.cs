using ECommerceAPI.Infrastructure;
using ECommerceBackend.API.Extensions;
using ECommerceBackend.Application;
using ECommerceBackend.Domain.Entities;
using ECommerceBackend.Persistence;
using ECommerceBackend.Persistence.Contexts;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddPersistenceServices();
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices();

builder.Services.AddCors(options => options.AddDefaultPolicy(policy =>
    policy.WithOrigins("http://localhost:4200", "https://localhost:4200").AllowAnyHeader().AllowAnyMethod().AllowCredentials()
));

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
      options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.Never;
    });

builder.Services.AddAuthorization();
builder.Services.AddIdentityApiEndpoints<AppUser>(options =>
            {
              options.Password.RequiredLength = 3;
              options.Password.RequireNonAlphanumeric = false;
              options.Password.RequireDigit = false;
              options.Password.RequireLowercase = false;
              options.Password.RequireUppercase = false;
            })
    .AddEntityFrameworkStores<ECommerceBackendDbContext>();

var app = builder.Build();

app.ConfigureExceptionHandler();

// Configure the HTTP request pipeline.
app.UseCors();

app.UseAuthorization();

app.MapControllers();
app.MapGroup("api").MapIdentityApi<AppUser>(); // api/login

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
