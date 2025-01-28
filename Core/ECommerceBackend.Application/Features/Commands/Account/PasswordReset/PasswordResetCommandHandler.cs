using ECommerceBackend.Application.Abstractions.Services;
using MediatR;

namespace ECommerceBackend.Application.Features.Commands.Account.PasswordReset
{
    public class PasswordResetCommandHandler(IAccountService accountService) : IRequestHandler<PasswordResetCommandRequest, PasswordResetCommandResponse>
    {
        readonly IAccountService _accountService = accountService;

        public async Task<PasswordResetCommandResponse> Handle(PasswordResetCommandRequest request, CancellationToken cancellationToken)
        {
            await _accountService.PasswordResetAsync(request.Email);
            return new();
        }
    }
}
