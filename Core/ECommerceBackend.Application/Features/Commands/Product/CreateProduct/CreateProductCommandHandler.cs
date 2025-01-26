using ECommerceBackend.Application.Abstractions.Services;
using ECommerceBackend.Application.Repositories;
using ECommerceBackend.Application.Repositories.Product;
using MediatR;

namespace ECommerceBackend.Application.Features.Commands.Product.CreateProduct
{
    public class CreateProductCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateProductCommandRequest, CreateProductCommandResponse>
    {
        readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<CreateProductCommandResponse> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
        {


            var product = new Domain.Entities.Product
            {
                Name = request.Name,
                Price = request.Price,
                Type = request.Type,
                PictureUrl = request.PictureUrl,
                Brand = request.Brand,
                QuantityInStock = request.QuantityInStock,
                Description = request.Description
            };

            await _unitOfWork.Repository<Domain.Entities.Product>().AddAsync(product);


            await _unitOfWork.Complete();

            return new()
            {
                Name = request.Name,
                Price = request.Price,
                Type = request.Type,
                PictureUrl = request.PictureUrl,
                Brand = request.Brand,
                QuantityInStock = request.QuantityInStock,
                Description = request.Description,
                Id = product.Id
            };
        }
    }
}
