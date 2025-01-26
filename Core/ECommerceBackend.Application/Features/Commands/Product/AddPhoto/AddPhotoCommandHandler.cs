using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloudinaryDotNet.Actions;
using ECommerceBackend.Application.Abstractions.Services;
using ECommerceBackend.Domain.Entities;
using MediatR;

namespace ECommerceBackend.Application.Features.Commands.Product.AddPhoto
{
    public class AddPhotoCommandHandler(IUnitOfWork unitOfWork, IPhotoService photoService) : IRequestHandler<AddPhotoCommandRequest, AddPhotoCommandResponse>
    {
        readonly IPhotoService _photoService = photoService;
        readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<AddPhotoCommandResponse> Handle(AddPhotoCommandRequest request, CancellationToken cancellationToken)
        {
            var product = await _unitOfWork.Repository<Domain.Entities.Product>().GetByIdAsync(request.ProductId);
            if (product == null) throw new Exception("there is no Product!!!");


            var result = await _photoService.AddPhotoAsync(request.File);

            if (result.Error != null) throw new Exception(result.Error.Message);

            var photo = new Photo
            {
                Url = result.SecureUrl.AbsoluteUri,
                PublicId = result.PublicId
            };

            product.PictureUrl = photo.Url;

            if (await _unitOfWork.Complete())
                return new()
                {
                    Id = photo.Id,
                    Url = photo.Url,
                    IsMain = photo.IsMain
                };

            throw new Exception("HATATATATATA!!!");
        }
    }
}