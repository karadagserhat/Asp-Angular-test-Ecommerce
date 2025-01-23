using MediatR;

namespace ECommerceBackend.Application.Features.Commands.Account.LogoutUser
{
    public class LogoutUserCommandRequest : IRequest<LogoutUserCommandResponse>
    {
    }
}