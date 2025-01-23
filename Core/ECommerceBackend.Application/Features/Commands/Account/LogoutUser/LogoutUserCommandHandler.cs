using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerceBackend.Application.Abstractions.Services;
using ECommerceBackend.Application.DTOs;
using ECommerceBackend.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ECommerceBackend.Application.Features.Commands.Account.LogoutUser
{
    public class LogoutUserCommandHandler(IAccountService accountService) : IRequestHandler<LogoutUserCommandRequest, LogoutUserCommandResponse>
    {
        readonly IAccountService _accountService = accountService;

        public async Task<LogoutUserCommandResponse> Handle(LogoutUserCommandRequest request, CancellationToken cancellationToken)
        {

            await _accountService.LogoutUserAsync();

            return new();
        }
    }
}