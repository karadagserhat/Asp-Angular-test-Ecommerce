using ECommerceBackend.API.RequestHelpers;
using ECommerceBackend.Application.DTOs;
using ECommerceBackend.Application.Features.Commands.Product.AddPhoto;
using ECommerceBackend.Application.Features.Commands.Product.CreateProduct;
using ECommerceBackend.Application.Features.Commands.Product.RemoveProduct;
using ECommerceBackend.Application.Features.Commands.Product.UpdateProduct;
using ECommerceBackend.Application.Features.Commands.Product.UpdateStock;
using ECommerceBackend.Application.Features.Queries.Product.GetAllProducts;
using ECommerceBackend.Application.Features.Queries.Product.GetByIdProduct;
using ECommerceBackend.Application.Features.Queries.Product.GetProductsBrands;
using ECommerceBackend.Application.Features.Queries.Product.GetProductsTypes;
using ECommerceBackend.Application.Repositories;
using ECommerceBackend.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceBackend.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController(IMediator mediator) : ControllerBase
{
  readonly IMediator _mediator = mediator;

  [Cache(600)]
  [HttpGet]
  public async Task<ActionResult<IEnumerable<Product>>> GetProducts([FromQuery] GetAllProductQueryRequest getAllProductQueryRequest)
  {
    GetAllProductQueryResponse response = await _mediator.Send(getAllProductQueryRequest);
    return Ok(response);
  }

  [Cache(600)]
  [HttpGet("{id:int}")]
  public async Task<ActionResult<Product>> GetProduct([FromRoute] GetByIdProductQueryRequest getByIdProductQueryRequest)
  {
    GetByIdProductQueryResponse response = await _mediator.Send(getByIdProductQueryRequest);
    return Ok(response);

  }

  [InvalidateCache("api/products|")]
  [Authorize(Roles = "Admin")]
  [HttpPost]
  public async Task<ActionResult<Product>> CreateProduct([FromBody] CreateProductCommandRequest createProductCommandRequest)
  {
    CreateProductCommandResponse response = await _mediator.Send(createProductCommandRequest);
    return Ok(response);
  }

  [InvalidateCache("api/products|")]
  [Authorize(Roles = "Admin")]
  [HttpPut("{id:int}")]
  public async Task<ActionResult> UpdateProduct([FromBody] UpdateProductCommandRequest updateProductCommandRequest)
  {
    UpdateProductCommandResponse response = await _mediator.Send(updateProductCommandRequest);
    return Ok();
  }

  [InvalidateCache("api/products|")]
  [Authorize(Roles = "Admin")]
  [HttpDelete("{id:int}")]
  public async Task<ActionResult> DeleteProduct([FromRoute] RemoveProductCommandRequest removeProductCommandRequest)
  {
    RemoveProductCommandResponse response = await _mediator.Send(removeProductCommandRequest);
    return Ok();
  }

  [Cache(10000)]
  [HttpGet("brands")]
  public async Task<ActionResult<IReadOnlyList<string>>> GetBrands()
  {
    GetProductsBrandsQueryResponse response = await _mediator.Send(new GetProductsBrandsQueryRequest());
    return Ok(response);

  }

  [Cache(10000)]
  [HttpGet("types")]
  public async Task<ActionResult<IReadOnlyList<string>>> GetTypes()
  {
    GetProductsTypesQueryResponse response = await _mediator.Send(new GetProductsTypesQueryRequest());
    return Ok(response);
  }

  [InvalidateCache("api/products|")]
  [Authorize(Roles = "Admin")]
  [HttpPut("update-stock/{productId}")]
  public async Task<ActionResult> UpdateStock([FromBody] UpdateStockCommandRequest updateStockCommandRequest)
  {
    UpdateStockCommandResponse response = await _mediator.Send(updateStockCommandRequest);
    return Ok(response);
  }

  [InvalidateCache("api/products|")]
  [Authorize(Roles = "Admin")]
  [HttpPost("add-photo")]
  public async Task<ActionResult<PhotoDTO>> AddPhoto([FromQuery] AddPhotoCommandRequest addPhotoCommandRequest)
  {
    addPhotoCommandRequest.File = Request.Form.Files[0];
    AddPhotoCommandResponse response = await _mediator.Send(addPhotoCommandRequest);
    return Ok(response);
  }
}

