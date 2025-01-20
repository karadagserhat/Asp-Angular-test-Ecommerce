using ECommerceBackend.Application.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ECommerceBackend.Application.Features.Queries.Product.GetProductsTypes
{
    public class GetProductsTypesQueryHandler(IProductReadRepository productReadRepository) : IRequestHandler<GetProductsTypesQueryRequest, GetProductsTypesQueryResponse>
    {
        readonly IProductReadRepository _productReadRepository = productReadRepository;

        public async Task<GetProductsTypesQueryResponse> Handle(GetProductsTypesQueryRequest request, CancellationToken cancellationToken)
        {
            var types = await _productReadRepository
            .GetAll()
            .Select(x => x.Type)
            .Distinct()
            .ToListAsync(cancellationToken);

            return new()
            {
                Types = types
            };
        }
    }
}
