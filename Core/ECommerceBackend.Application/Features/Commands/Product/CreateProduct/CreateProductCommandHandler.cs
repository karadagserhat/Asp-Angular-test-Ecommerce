using ECommerceBackend.Application.Abstractions.Hubs;
using ECommerceBackend.Application.Abstractions.Services;
using MediatR;

namespace ECommerceBackend.Application.Features.Commands.Product.CreateProduct
{
    public class CreateProductCommandHandler(IUnitOfWork unitOfWork, IProductHubService productHubService) : IRequestHandler<CreateProductCommandRequest, CreateProductCommandResponse>
    {
        readonly IUnitOfWork _unitOfWork = unitOfWork;
        readonly IProductHubService _productHubService = productHubService;

        public async Task<CreateProductCommandResponse> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
        {


            var product = new Domain.Entities.Product
            {
                Name = request.Name,
                Price = request.Price,
                Type = request.Type,
                PictureUrl = request.PictureUrl,
                Brand = request.Brand,
                QuantityInStock = request.QuantityInStock,
                Description = request.Description
            };

            await _unitOfWork.Repository<Domain.Entities.Product>().AddAsync(product);


            await _unitOfWork.Complete();

            await _productHubService.ProductAddedMessageAsync($"{request.UserEmail} ...... {request.Name} added product!!!");

            return new()
            {
                Name = request.Name,
                Price = request.Price,
                Type = request.Type,
                PictureUrl = request.PictureUrl,
                Brand = request.Brand,
                QuantityInStock = request.QuantityInStock,
                Description = request.Description,
                Id = product.Id
            };
        }
    }
}
