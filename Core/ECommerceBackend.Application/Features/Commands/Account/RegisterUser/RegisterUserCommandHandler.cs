using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerceBackend.Application.Abstractions.Services;
using ECommerceBackend.Application.DTOs;
using ECommerceBackend.Application.Exceptions;
using ECommerceBackend.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ECommerceBackend.Application.Features.Commands.Account.RegisterUser
{
    public class RegisterUserCommandHandler(IAccountService accountService) : IRequestHandler<RegisterUserCommandRequest, RegisterUserCommandResponse>
    {
        readonly IAccountService _accountService = accountService;

        public async Task<RegisterUserCommandResponse> Handle(RegisterUserCommandRequest request, CancellationToken cancellationToken)
        {

            var registerDto = new RegisterDTO
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Password = request.Password
            };

            var result = await _accountService.RegisterUserAsync(registerDto);

            if (!result.Succeeded)
            {
                throw new AuthenticationErrorException(result.Message);
            }

            return new()
            {
                Message = result.Message,
                Succeeded = result.Succeeded,
            };
        }
    }
}