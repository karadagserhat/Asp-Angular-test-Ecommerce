using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ECommerceBackend.Application.DTOs;
using MediatR;

namespace ECommerceBackend.Application.Features.Commands.Account.CreateOrUpdateAddress
{
    public class CreateOrUpdateAddressCommandRequest : IRequest<CreateOrUpdateAddressCommandResponse>
    {
        public required AddressDto AddressDto { get; set; }
    }
}