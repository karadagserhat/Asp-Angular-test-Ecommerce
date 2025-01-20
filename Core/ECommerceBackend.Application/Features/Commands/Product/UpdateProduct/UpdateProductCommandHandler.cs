using ECommerceBackend.Application.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ECommerceBackend.Application.Features.Commands.Product.UpdateProduct
{
    public class UpdateProductCommandHandler(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository) : IRequestHandler<UpdateProductCommandRequest, UpdateProductCommandResponse>
    {
        readonly IProductReadRepository _productReadRepository = productReadRepository;
        readonly IProductWriteRepository _productWriteRepository = productWriteRepository;

        public async Task<UpdateProductCommandResponse> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
        {
            Domain.Entities.Product product = await _productReadRepository.GetByIdAsync(request.Id);

            product.Name = request.Name;
            product.Price = request.Price;
            product.Type = request.Type;
            product.PictureUrl = request.PictureUrl;
            product.Brand = request.Brand;
            product.QuantityInStock = request.QuantityInStock;
            product.Description = request.Description;

            await _productWriteRepository.SaveAsync();

            return new();
        }
    }
}
