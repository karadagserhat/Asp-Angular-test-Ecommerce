using ECommerceBackend.Application.Abstractions.Services;
using ECommerceBackend.Application.Exceptions;
using ECommerceBackend.Application.Features.Commands.Cart.UpdateCart;
using ECommerceBackend.Application.Repositories;
using ECommerceBackend.Domain.Entities;
using MediatR;
using P = ECommerceBackend.Domain.Entities;

namespace ECommerceBackend.Application.Features.Queries.Cart.UpdateCart
{
    internal class UpdateCartCommandHandler(ICartService cartService) : IRequestHandler<UpdateCartCommandRequest, UpdateCartCommandResponse>
    {

        readonly ICartService _cartService = cartService;

        public async Task<UpdateCartCommandResponse> Handle(UpdateCartCommandRequest request, CancellationToken cancellationToken)
        {
            var shoppingCart = new ShoppingCart
            {
                Id = request.Id,
                Items = request.Items,
                ClientSecret = request.ClientSecret,
                PaymentIntentId = request.PaymentIntentId,
                DeliveryMethodId = request.DeliveryMethodId,
            };

            var updatedCart = await _cartService.SetCartAsync(shoppingCart) ?? throw new CartUpdateFailedException("Problem with cart");

            return new UpdateCartCommandResponse
            {
                Id = updatedCart.Id,
                Items = updatedCart.Items.Select(item => new CartItem
                {
                    ProductId = item.ProductId,
                    ProductName = item.ProductName,
                    Price = item.Price,
                    Quantity = item.Quantity,
                    PictureUrl = item.PictureUrl,
                    Brand = item.Brand,
                    Type = item.Type
                }).ToList(),
                ClientSecret = updatedCart.ClientSecret,
                PaymentIntentId = updatedCart.PaymentIntentId,
                DeliveryMethodId = updatedCart.DeliveryMethodId,
            };
        }
    }
}
