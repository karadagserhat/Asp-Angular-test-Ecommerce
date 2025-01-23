using ECommerceBackend.Application.Abstractions.Services;
using MediatR;

namespace ECommerceBackend.Application.Features.Queries.Account.GetAuthState
{
    internal class GetAuthStateQueryHandler(IAccountService accountService) : IRequestHandler<GetAuthStateQueryRequest, GetAuthStateQueryResponse>
    {

        readonly IAccountService _accountService = accountService;

        public async Task<GetAuthStateQueryResponse> Handle(GetAuthStateQueryRequest request, CancellationToken cancellationToken)
        {

            var data = _accountService.GetAuthState();


            return new GetAuthStateQueryResponse
            {
                IsAuthenticated = data.IsAuthenticated
            };
        }
    }
}
