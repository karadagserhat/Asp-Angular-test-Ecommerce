using ECommerceBackend.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ECommerceBackend.Application.Repositories;
using ECommerceBackend.Persistence.Repositories;

namespace ECommerceBackend.Persistence;

public static class ServiceRegistration
{
  public static void AddPersistenceServices(this IServiceCollection services)
  {
    services.AddDbContext<ECommerceBackendDbContext>(opt => opt.UseSqlServer(Configuration.ConnectionString));

    services.AddScoped<IProductReadRepository, ProductReadRepository>();
    services.AddScoped<IProductWriteRepository, ProductWriteRepository>();
  }
}
