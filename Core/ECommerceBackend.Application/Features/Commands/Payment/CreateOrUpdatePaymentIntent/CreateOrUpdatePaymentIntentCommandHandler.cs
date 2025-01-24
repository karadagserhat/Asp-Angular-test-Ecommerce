using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerceBackend.Application.Abstractions.Services;
using ECommerceBackend.Application.Exceptions;
using MediatR;

namespace ECommerceBackend.Application.Features.Commands.Payment.CreateOrUpdatePaymentIntent
{
    public class CreateOrUpdatePaymentIntentCommandHandler(IPaymentService paymentService) : IRequestHandler<CreateOrUpdatePaymentIntentCommandRequest, CreateOrUpdatePaymentIntentCommandResponse>
    {
        public async Task<CreateOrUpdatePaymentIntentCommandResponse> Handle(CreateOrUpdatePaymentIntentCommandRequest request, CancellationToken cancellationToken)
        {
            var cart = await paymentService.CreateOrUpdatePaymentIntent(request.CartId) ?? throw new CreateOrUpdatePaymentIntentErrorException("Problem with cart");

            return new()
            {
                Id = cart.Id,
                Items = cart.Items,
                ClientSecret = cart.ClientSecret,
                PaymentIntentId = cart.PaymentIntentId
            };
        }
    }
}