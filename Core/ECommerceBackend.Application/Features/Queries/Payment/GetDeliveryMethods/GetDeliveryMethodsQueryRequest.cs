using ECommerceBackend.Application.DTOs;
using MediatR;

namespace ECommerceBackend.Application.Features.Queries.Payment.GetDeliveryMethods
{
    public class GetDeliveryMethodsQueryRequest : IRequest<List<DeliveryMethodDto>>
    {

    }
}
