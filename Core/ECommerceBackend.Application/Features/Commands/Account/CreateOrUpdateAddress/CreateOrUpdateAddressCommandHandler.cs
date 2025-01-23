using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerceBackend.Application.Abstractions.Services;
using ECommerceBackend.Application.DTOs;
using ECommerceBackend.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ECommerceBackend.Application.Features.Commands.Account.CreateOrUpdateAddress
{
    public class CreateOrUpdateAddressCommandHandler(IAccountService accountService) : IRequestHandler<CreateOrUpdateAddressCommandRequest, CreateOrUpdateAddressCommandResponse>
    {
        readonly IAccountService _accountService = accountService;

        public async Task<CreateOrUpdateAddressCommandResponse> Handle(CreateOrUpdateAddressCommandRequest request, CancellationToken cancellationToken)
        {
            // var AddressDto = new AddressDto
            // {
            //     Line1 = request.Line1,
            //     Line2 = request.Line2,
            //     City = request.City,
            //     State = request.State,
            //     Country = request.Country,
            //     PostalCode = request.PostalCode,
            // };

            var response = await _accountService.CreateOrUpdateAddress(request.AddressDto);

            return new()
            {
                Line1 = response.Line1,
                Line2 = response.Line2,
                City = response.City,
                State = response.State,
                Country = response.Country,
                PostalCode = response.PostalCode,
            };
        }
    }
}