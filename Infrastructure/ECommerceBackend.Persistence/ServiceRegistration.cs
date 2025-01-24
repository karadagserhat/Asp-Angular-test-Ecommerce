using ECommerceBackend.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ECommerceBackend.Application.Repositories;
using ECommerceBackend.Persistence.Repositories;
using ECommerceBackend.Application.Abstractions.Services;
using ECommerceBackend.Persistence.Services;

namespace ECommerceBackend.Persistence;

public static class ServiceRegistration
{
  public static void AddPersistenceServices(this IServiceCollection services)
  {
    services.AddDbContext<ECommerceBackendDbContext>(opt => opt.UseSqlServer(Configuration.ConnectionString));

    services.AddScoped<IProductReadRepository, ProductReadRepository>();
    services.AddScoped<IProductWriteRepository, ProductWriteRepository>();
    services.AddScoped<IDeliveryMethodReadRepository, DeliveryMethodReadRepository>();
    services.AddScoped<IDeliveryMethodWriteRepository, DeliveryMethodWriteRepository>();
    services.AddScoped<IAccountService, AccountService>();

    services.AddHttpContextAccessor();

  }
}
