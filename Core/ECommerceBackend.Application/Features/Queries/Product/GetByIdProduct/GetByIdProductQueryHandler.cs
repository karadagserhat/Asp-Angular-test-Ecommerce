using ECommerceBackend.Application.Abstractions.Services;
using ECommerceBackend.Application.Exceptions;
using ECommerceBackend.Application.Repositories;
using ECommerceBackend.Application.Repositories.Product;
using MediatR;
using P = ECommerceBackend.Domain.Entities;

namespace ECommerceBackend.Application.Features.Queries.Product.GetByIdProduct
{
    internal class GetByIdProductQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetByIdProductQueryRequest, GetByIdProductQueryResponse>
    {

        readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<GetByIdProductQueryResponse> Handle(GetByIdProductQueryRequest request, CancellationToken cancellationToken)
        {
            P.Product? product = await _unitOfWork.Repository<Domain.Entities.Product>().GetByIdAsync(request.Id, false) ?? throw new ProductGetFailedException();

            return new()
            {
                Name = product.Name,
                Price = product.Price,
                Type = product.Type,
                PictureUrl = product.PictureUrl,
                Brand = product.Brand,
                QuantityInStock = product.QuantityInStock,
                Description = product.Description
            };
        }
    }
}
