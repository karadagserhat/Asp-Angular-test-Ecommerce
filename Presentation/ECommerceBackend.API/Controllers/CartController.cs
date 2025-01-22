using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerceBackend.Application.Features.Commands.Cart.DeleteCart;
using ECommerceBackend.Application.Features.Commands.Cart.UpdateCart;
using ECommerceBackend.Application.Features.Queries.Cart.GetCartById;
using ECommerceBackend.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceBackend.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController(IMediator mediator) : ControllerBase
    {
        readonly IMediator _mediator = mediator;

        [HttpGet]
        public async Task<ActionResult<ShoppingCart>> GetCartById([FromQuery] GetCartByIdQueryRequest getCartByIdQueryRequest)
        {
            GetCartByIdQueryResponse response = await _mediator.Send(getCartByIdQueryRequest);
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<ShoppingCart>> UpdateCart([FromBody] UpdateCartCommandRequest updateCartCommandRequest)
        {
            UpdateCartCommandResponse response = await _mediator.Send(updateCartCommandRequest);
            return Ok(response);
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteCart([FromQuery] DeleteCartCommandRequest deleteCartCommandRequest)
        {
            DeleteCartCommandResponse response = await _mediator.Send(deleteCartCommandRequest);
            return Ok(response);
        }

    }
}