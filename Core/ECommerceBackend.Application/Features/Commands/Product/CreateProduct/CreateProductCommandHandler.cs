using ECommerceBackend.Application.Repositories;
using MediatR;

namespace ECommerceBackend.Application.Features.Commands.Product.CreateProduct
{
    public class CreateProductCommandHandler(IProductWriteRepository productWriteRepository) : IRequestHandler<CreateProductCommandRequest, CreateProductCommandResponse>
    {
        readonly IProductWriteRepository _productWriteRepository = productWriteRepository;

        public async Task<CreateProductCommandResponse> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
        {
            await _productWriteRepository.AddAsync(new()
            {
                Name = request.Name,
                Price = request.Price,
                Type = request.Type,
                PictureUrl = request.PictureUrl,
                Brand = request.Brand,
                QuantityInStock = request.QuantityInStock,
                Description = request.Description
            });

            await _productWriteRepository.SaveAsync();

            return new()
            {
                Name = request.Name,
                Price = request.Price,
                Type = request.Type,
                PictureUrl = request.PictureUrl,
                Brand = request.Brand,
                QuantityInStock = request.QuantityInStock,
                Description = request.Description
            };
        }
    }
}
