using ECommerceBackend.Application.Repositories;
using ECommerceBackend.Domain.Entities;
using ECommerceBackend.Persistence.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerceBackend.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController(ECommerceBackendDbContext context, IProductWriteRepository productWriteRepository, IProductReadRepository productReadRepository) : ControllerBase
{
  private readonly ECommerceBackendDbContext context = context;
  private readonly IProductWriteRepository _productWriteRepository = productWriteRepository;
  private readonly IProductReadRepository _productReadRepository = productReadRepository;

  [HttpGet]
  public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
  {
    var products = await _productReadRepository.GetAll().ToListAsync();
    return Ok(products);

  }

  [HttpGet("{id:int}")] // api/products/2
  public async Task<ActionResult<Product>> GetProduct(int id)
  {
    var product = await _productReadRepository.GetByIdAsync(id);
    if (product == null) return NotFound();
    return product;
  }
  [HttpPost]
  public async Task<ActionResult<Product>> CreateProduct(Product product)
  {
    await _productWriteRepository.AddAsync(product);
    await _productWriteRepository.SaveAsync();
    return product;
  }
  [HttpPut("{id:int}")]
  public async Task<ActionResult> UpdateProduct(int id, Product product)
  {
    if (product.Id != id || !ProductExists(id))
      return BadRequest("Cannot update this product");

    _productWriteRepository.Update(product);
    await _productWriteRepository.SaveAsync();
    return NoContent();
  }
  [HttpDelete("{id:int}")]
  public async Task<ActionResult> DeleteProduct(int id)
  {
    var product = await _productReadRepository.GetByIdAsync(id);
    if (product == null) return NotFound();

    await _productWriteRepository.RemoveAsync(id);
    await _productWriteRepository.SaveAsync();

    return NoContent();
  }

  [HttpGet("brands")]
  public async Task<ActionResult<IReadOnlyList<string>>> GetBrands()
  {
    var brands = await _productReadRepository.GetSelect(x => x.Brand)
                          .Distinct()
                          .ToListAsync();

    return Ok(brands);

  }


  // [HttpGet("types")]
  // public async Task<ActionResult<IReadOnlyList<string>>> GetTypes()
  // {
  //   return Ok(await repo.GetTypesAsync());
  // }

  private bool ProductExists(int id)
  {
    return _productReadRepository.GetWhere(x => x.Id == id).Any();
  }
}