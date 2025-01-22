using ECommerceBackend.Application.Abstractions.Services;
using ECommerceBackend.Application.Exceptions;
using ECommerceBackend.Application.Repositories;
using ECommerceBackend.Domain.Entities;
using MediatR;
using P = ECommerceBackend.Domain.Entities;

namespace ECommerceBackend.Application.Features.Queries.Cart.GetCartById
{
    internal class GetCartByIdQueryHandler(ICartService cartService) : IRequestHandler<GetCartByIdQueryRequest, GetCartByIdQueryResponse>
    {

        readonly ICartService _cartService = cartService;

        public async Task<GetCartByIdQueryResponse> Handle(GetCartByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var cart = await _cartService.GetCartAsync(request.Id);

            cart ??= new P.ShoppingCart
            {
                Id = request.Id,
                Items = new List<P.CartItem>()
            };

            return new GetCartByIdQueryResponse
            {
                Id = cart.Id,
                Items = cart.Items.Select(item => new CartItem
                {
                    ProductId = item.ProductId,
                    ProductName = item.ProductName,
                    Price = item.Price,
                    Quantity = item.Quantity,
                    PictureUrl = item.PictureUrl,
                    Brand = item.Brand,
                    Type = item.Type
                }).ToList()
            };
        }
    }
}
