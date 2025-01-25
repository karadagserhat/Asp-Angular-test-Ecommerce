using System.Text.Json;
using ECommerceBackend.Domain.Entities;
using ECommerceBackend.Persistence.Contexts;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Data;
public class StoreContextSeed
{
  public static async Task SeedAsync(ECommerceBackendDbContext context, UserManager<AppUser> userManager)
  {
    if (!userManager.Users.Any(x => x.UserName == "admin@test.com"))
    {
      var user = new AppUser
      {
        UserName = "admin@test.com",
        Email = "admin@test.com",
      };
      await userManager.CreateAsync(user, "Pa$$w0rd");
      await userManager.AddToRoleAsync(user, "Admin");
    }

    if (!context.Products.Any())
    {
      var productsData = await File.ReadAllTextAsync("../ECommerceBackend.API/SeedData/products.json");

      var products = JsonSerializer.Deserialize<List<Product>>(productsData);
      if (products == null) return;
      context.Products.AddRange(products);
      await context.SaveChangesAsync();
    }
  }
}