using ECommerceBackend.Application.Abstractions.Services;
using ECommerceBackend.Application.Exceptions;
using MediatR;

namespace ECommerceBackend.Application.Features.Commands.Account.UpdatePassword
{
    public class UpdatePasswordCommandHandler(IAccountService accountService) : IRequestHandler<UpdatePasswordCommandRequest, UpdatePasswordCommandResponse>
    {
        readonly IAccountService _accountService = accountService;
        public async Task<UpdatePasswordCommandResponse> Handle(UpdatePasswordCommandRequest request, CancellationToken cancellationToken)
        {
            if (!request.Password.Equals(request.PasswordConfirm))
                throw new PasswordChangeFailedException("Lütfen şifreyi birebir doğrulayınız.");

            await _accountService.UpdatePasswordAsync(request.UserId, request.ResetToken, request.Password);
            return new();
        }
    }
}
