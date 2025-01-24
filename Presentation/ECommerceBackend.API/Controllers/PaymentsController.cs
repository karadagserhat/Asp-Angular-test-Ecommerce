using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerceBackend.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ECommerceBackend.Application.Features.Commands.Payment.CreateOrUpdatePaymentIntent;
using MediatR;
using ECommerceBackend.Application.Features.Queries.Payment.GetDeliveryMethods;
using ECommerceBackend.Application.DTOs;

namespace ECommerceBackend.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentsController(IMediator mediator) : ControllerBase
    {
        readonly IMediator _mediator = mediator;

        [Authorize]
        [HttpPost("{cartId}")]
        public async Task<ActionResult<ShoppingCart>> CreateOrUpdatePaymentIntent([FromRoute] CreateOrUpdatePaymentIntentCommandRequest createOrUpdatePaymentIntentCommandRequest)
        {
            CreateOrUpdatePaymentIntentCommandResponse response = await _mediator.Send(createOrUpdatePaymentIntentCommandRequest);
            return Ok(response);
        }

        [HttpGet("delivery-methods")]
        public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethods()
        {
            List<DeliveryMethodDto> response = await _mediator.Send(new GetDeliveryMethodsQueryRequest());
            return Ok(response);
        }
    }
}