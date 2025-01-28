using ECommerceBackend.Application.Abstractions.Services;
using MediatR;

namespace ECommerceBackend.Application.Features.Commands.Account.VerifyResetToken
{
    public class VerifyResetTokenCommandHandler(IAccountService accountService) : IRequestHandler<VerifyResetTokenCommandRequest, VerifyResetTokenCommandResponse>
    {
        readonly IAccountService _accountService = accountService;

        public async Task<VerifyResetTokenCommandResponse> Handle(VerifyResetTokenCommandRequest request, CancellationToken cancellationToken)
        {
            bool state = await _accountService.VerifyResetTokenAsync(request.ResetToken, request.UserId);
            return new()
            {
                State = state
            };
        }
    }
}
