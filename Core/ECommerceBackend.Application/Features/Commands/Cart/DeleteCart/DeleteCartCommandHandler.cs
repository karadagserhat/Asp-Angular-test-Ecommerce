using ECommerceBackend.Application.Abstractions.Services;
using ECommerceBackend.Application.Exceptions;
using ECommerceBackend.Application.Features.Commands.Cart.DeleteCart;
using ECommerceBackend.Application.Repositories;
using ECommerceBackend.Domain.Entities;
using MediatR;
using P = ECommerceBackend.Domain.Entities;

namespace ECommerceBackend.Application.Features.Queries.Cart.DeleteCart
{
    internal class DeleteCartCommandHandler(ICartService cartService) : IRequestHandler<DeleteCartCommandRequest, DeleteCartCommandResponse>
    {

        readonly ICartService _cartService = cartService;

        public async Task<DeleteCartCommandResponse> Handle(DeleteCartCommandRequest request, CancellationToken cancellationToken)
        {
            var result = await _cartService.DeleteCartAsync(request.Id);

            if (!result) throw new CartDeleteFailedException("Problem deleting cart");

            return new()
            {

            };
        }
    }
}
