using ECommerceBackend.Application.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ECommerceBackend.Application.Features.Queries.Product.GetAllProducts
{
    public class GetAllProductQueryHandler(IProductReadRepository productReadRepository) : IRequestHandler<GetAllProductQueryRequest, GetAllProductQueryResponse>
    {
        readonly IProductReadRepository _productReadRepository = productReadRepository;

        public async Task<GetAllProductQueryResponse> Handle(GetAllProductQueryRequest request, CancellationToken cancellationToken)
        {
            var query = _productReadRepository.GetAll().AsQueryable();


            if (request.Brands.Count != 0)
                query = query.Where(x => request.Brands.Contains(x.Brand));

            if (request.Types.Count != 0)
                query = query.Where(x => request.Types.Contains(x.Type));

            query = query.Skip(request.PageSize * (request.PageIndex - 1)).Take(request.PageSize);
            var count = await query.CountAsync(cancellationToken);

            query = request.Sort switch
            {
                "priceAsc" => query.OrderBy(x => x.Price),
                "priceDesc" => query.OrderByDescending(x => x.Price),
                _ => query.OrderBy(x => x.Name)
            };

            var products = await query.ToListAsync(cancellationToken);

            return new()
            {
                Products = products,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Count = count

            };
        }
    }
}
