using ECommerceBackend.Application.Exceptions;
using ECommerceBackend.Application.Repositories;
using MediatR;
using P = ECommerceBackend.Domain.Entities;

namespace ECommerceBackend.Application.Features.Queries.Product.GetByIdProduct
{
    internal class GetByIdProductQueryHandler(IProductReadRepository productReadRepository) : IRequestHandler<GetByIdProductQueryRequest, GetByIdProductQueryResponse>
    {

        readonly IProductReadRepository _productReadRepository = productReadRepository;

        public async Task<GetByIdProductQueryResponse> Handle(GetByIdProductQueryRequest request, CancellationToken cancellationToken)
        {
            P.Product? product = await _productReadRepository.GetByIdAsync(request.Id, false) ?? throw new ProductGetFailedException();

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
