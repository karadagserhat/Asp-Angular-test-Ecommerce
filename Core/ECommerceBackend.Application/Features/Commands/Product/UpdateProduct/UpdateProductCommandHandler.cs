using ECommerceBackend.Application.Abstractions.Services;
using ECommerceBackend.Application.Exceptions;
using ECommerceBackend.Application.Repositories;
using ECommerceBackend.Application.Repositories.Product;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ECommerceBackend.Application.Features.Commands.Product.UpdateProduct
{
    public class UpdateProductCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateProductCommandRequest, UpdateProductCommandResponse>
    {
        readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<UpdateProductCommandResponse> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
        {
            Domain.Entities.Product? product = await _unitOfWork.Repository<Domain.Entities.Product>().GetByIdAsync(request.Id) ?? throw new ProductUpdateFailedException();

            product.Name = request.Name;
            product.Price = request.Price;
            product.Type = request.Type;
            product.PictureUrl = request.PictureUrl;
            product.Brand = request.Brand;
            product.QuantityInStock = request.QuantityInStock;
            product.Description = request.Description;

            await _unitOfWork.Complete();

            return new();
        }
    }
}
