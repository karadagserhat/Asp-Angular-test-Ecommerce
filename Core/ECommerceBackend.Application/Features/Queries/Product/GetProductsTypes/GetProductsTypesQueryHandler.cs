using ECommerceBackend.Application.Abstractions.Services;
using ECommerceBackend.Application.Repositories;
using ECommerceBackend.Application.Repositories.Product;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ECommerceBackend.Application.Features.Queries.Product.GetProductsTypes
{
    public class GetProductsTypesQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetProductsTypesQueryRequest, GetProductsTypesQueryResponse>
    {
        readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<GetProductsTypesQueryResponse> Handle(GetProductsTypesQueryRequest request, CancellationToken cancellationToken)
        {
            var types = await _unitOfWork.Repository<Domain.Entities.Product>()
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
