using MediatR;

namespace ECommerceBackend.Application.Features.Commands.Product.UpdateStock
{
    public class UpdateStockCommandRequest : IRequest<UpdateStockCommandResponse>
    {
        public int ProductId { get; set; }
        public int NewQuantity { get; set; }
    }
}
