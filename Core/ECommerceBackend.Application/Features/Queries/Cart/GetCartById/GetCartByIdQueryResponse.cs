using ECommerceBackend.Domain.Entities;

namespace ECommerceBackend.Application.Features.Queries.Cart.GetCartById
{
    public class GetCartByIdQueryResponse
    {
        public required string Id { get; set; }
        public List<CartItem> Items { get; set; } = [];
    }
}
