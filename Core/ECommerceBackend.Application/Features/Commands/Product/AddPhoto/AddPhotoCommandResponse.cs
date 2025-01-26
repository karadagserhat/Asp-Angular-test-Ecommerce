using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceBackend.Application.Features.Commands.Product.AddPhoto
{
    public class AddPhotoCommandResponse
    {
        public int Id { get; set; }
        public string? Url { get; set; }
        public bool IsMain { get; set; }
    }
}