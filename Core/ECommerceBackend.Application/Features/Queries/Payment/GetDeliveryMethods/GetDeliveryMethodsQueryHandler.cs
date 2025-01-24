using System.Runtime.CompilerServices;
using ECommerceBackend.Application.DTOs;
using ECommerceBackend.Application.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ECommerceBackend.Application.Features.Queries.Payment.GetDeliveryMethods
{
    public class GetDeliveryMethodsQueryHandler(IDeliveryMethodReadRepository deliveryMethodReadRepository)
         : IRequestHandler<GetDeliveryMethodsQueryRequest, List<DeliveryMethodDto>>
    {
        readonly IDeliveryMethodReadRepository _deliveryMethodReadRepository = deliveryMethodReadRepository;

        public async Task<List<DeliveryMethodDto>> Handle(GetDeliveryMethodsQueryRequest request, CancellationToken cancellationToken)
        {
            var data = await _deliveryMethodReadRepository.GetAll().ToListAsync(cancellationToken: cancellationToken);

            return data.Select(x => new DeliveryMethodDto
            {
                ShortName = x.ShortName,
                DeliveryTime = x.DeliveryTime,
                Description = x.Description,
                Price = x.Price
            }).ToList();
        }
    }
}
