using ECommerceBackend.Application.Abstractions.Services;
using ECommerceBackend.Application.Exceptions;
using ECommerceBackend.Application.Repositories;
using ECommerceBackend.Domain.Entities;
using MediatR;
using P = ECommerceBackend.Domain.Entities;

namespace ECommerceBackend.Application.Features.Queries.Account.GetUserInfo
{
    internal class GetUserInfoQueryHandler(IAccountService accountService) : IRequestHandler<GetUserInfoQueryRequest, GetUserInfoQueryResponse>
    {

        readonly IAccountService _accountService = accountService;

        public async Task<GetUserInfoQueryResponse> Handle(GetUserInfoQueryRequest request, CancellationToken cancellationToken)
        {
            var account = await _accountService.GetUserInfoAsync();

            if (account == null)
            {
                return null!;
            }

            return new GetUserInfoQueryResponse
            {
                FirstName = account.FirstName,
                LastName = account.LastName,
                Email = account.Email,
                Address = account.Address,
                Roles = account.Roles
            };
        }
    }
}
