using ECommerceBackend.Application.Abstractions.Services;
using ECommerceBackend.Application.Repositories;
using ECommerceBackend.Application.Repositories.Product;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ECommerceBackend.Application.Features.Queries.Product.GetProductsBrands
{
    public class GetProductsBrandsQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetProductsBrandsQueryRequest, GetProductsBrandsQueryResponse>
    {
        readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<GetProductsBrandsQueryResponse> Handle(GetProductsBrandsQueryRequest request, CancellationToken cancellationToken)
        {
            var brands = await _unitOfWork.Repository<Domain.Entities.Product>()
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
