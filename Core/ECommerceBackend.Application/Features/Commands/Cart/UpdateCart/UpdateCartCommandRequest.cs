using ECommerceBackend.Domain.Entities;
using MediatR;

namespace ECommerceBackend.Application.Features.Commands.Cart.UpdateCart
{
    public class UpdateCartCommandRequest : IRequest<UpdateCartCommandResponse>
    {
        public required string Id { get; set; }
        public List<CartItem> Items { get; set; } = [];
    }
}
