using MediatR;

namespace ECommerceBackend.Application.Features.Queries.Cart.GetCartById
{
    public class GetCartByIdQueryRequest : IRequest<GetCartByIdQueryResponse>
    {
        public required string Id { get; set; }
    }
}
