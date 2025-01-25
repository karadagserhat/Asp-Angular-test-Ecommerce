using ECommerceBackend.Application.Abstractions.Services;
using ECommerceBackend.Application.Repositories;
using ECommerceBackend.Application.Repositories.Product;
using MediatR;

namespace ECommerceBackend.Application.Features.Commands.Product.RemoveProduct
{
    public class RemoveProductCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<RemoveProductCommandRequest, RemoveProductCommandResponse>
    {
        readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<RemoveProductCommandResponse> Handle(RemoveProductCommandRequest request, CancellationToken cancellationToken)
        {
            await _unitOfWork.Repository<Domain.Entities.Product>().RemoveAsync(request.Id);
            await _unitOfWork.Complete();

            return new();
        }
    }
}
