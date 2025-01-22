using ECommerceBackend.Domain.Entities;
using MediatR;

namespace ECommerceBackend.Application.Features.Commands.Cart.DeleteCart
{
    public class DeleteCartCommandRequest : IRequest<DeleteCartCommandResponse>
    {
        public required string Id { get; set; }
        public List<CartItem> Items { get; set; } = [];
    }
}
