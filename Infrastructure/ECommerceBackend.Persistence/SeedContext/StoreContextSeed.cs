using System.Text.Json;
using ECommerceBackend.Domain.Entities;
using ECommerceBackend.Persistence.Contexts;

namespace Infrastructure.Data;
public class StoreContextSeed
{
  public static async Task SeedAsync(ECommerceBackendDbContext context)
  {
    if (!context.Products.Any())
    {
      var productsData = await File.ReadAllTextAsync("../ECommerceBackend.API/SeedData/products.json");

      var products = JsonSerializer.Deserialize<List<Product>>(productsData);
      if (products == null) return;
      context.Products.AddRange(products);
      await context.SaveChangesAsync();
    }

    if (!context.DeliveryMethods.Any())
    {
      var dmData = await File.ReadAllTextAsync("../ECommerceBackend.API/SeedData/delivery.json");
      var methods = JsonSerializer.Deserialize<List<DeliveryMethod>>(dmData);
      if (methods == null) return;
      context.DeliveryMethods.AddRange(methods);
      await context.SaveChangesAsync();
    }
  }
}