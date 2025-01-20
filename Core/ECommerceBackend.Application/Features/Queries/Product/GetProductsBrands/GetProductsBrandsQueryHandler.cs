using ECommerceBackend.Application.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ECommerceBackend.Application.Features.Queries.Product.GetProductsBrands
{
    public class GetProductsBrandsQueryHandler(IProductReadRepository productReadRepository) : IRequestHandler<GetProductsBrandsQueryRequest, GetProductsBrandsQueryResponse>
    {
        readonly IProductReadRepository _productReadRepository = productReadRepository;

        public async Task<GetProductsBrandsQueryResponse> Handle(GetProductsBrandsQueryRequest request, CancellationToken cancellationToken)
        {
            var brands = await _productReadRepository
            .GetAll()
            .Select(x => x.Brand)
            .Distinct()
            .ToListAsync(cancellationToken);

            return new()
            {
                Brands = brands
            };
        }
    }
}
