using ECommerceBackend.Application.Abstractions.Services;
using ECommerceBackend.Application.Exceptions;
using ECommerceBackend.Application.Repositories;
using ECommerceBackend.Application.Repositories.Product;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ECommerceBackend.Application.Features.Commands.Product.UpdateStock
{
    public class UpdateStockCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateStockCommandRequest, UpdateStockCommandResponse>
    {
        readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<UpdateStockCommandResponse> Handle(UpdateStockCommandRequest request, CancellationToken cancellationToken)
        {

            Domain.Entities.Product productItem = await _unitOfWork.Repository<Domain.Entities.Product>().GetByIdAsync(request.ProductId) ?? throw new ProductGetFailedException();

            productItem.QuantityInStock = request.NewQuantity;

            _unitOfWork.Repository<Domain.Entities.Product>().Update(productItem);


            if (await _unitOfWork.Complete())
            {
                return new();

            }

            throw new ProductUpdateFailedException("Problem updating stock");
        }
    }
}
