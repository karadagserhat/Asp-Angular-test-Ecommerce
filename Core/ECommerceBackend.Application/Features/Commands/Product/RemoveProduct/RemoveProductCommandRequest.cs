using MediatR;

namespace ECommerceBackend.Application.Features.Commands.Product.RemoveProduct
{
    public class RemoveProductCommandRequest : IRequest<RemoveProductCommandResponse>
    {
        public int Id { get; set; }
    }
}
