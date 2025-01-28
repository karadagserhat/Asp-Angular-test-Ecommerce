using ECommerceBackend.Application.Abstractions.Services;
using ECommerceBackend.Application.DTOs;
using ECommerceBackend.Application.Exceptions;
using ECommerceBackend.Application.Features.Commands.Account.CreateOrUpdateAddress;
using ECommerceBackend.Application.Features.Commands.Account.LogoutUser;
using ECommerceBackend.Application.Features.Commands.Account.PasswordReset;
using ECommerceBackend.Application.Features.Commands.Account.RegisterUser;
using ECommerceBackend.Application.Features.Commands.Account.UpdatePassword;
using ECommerceBackend.Application.Features.Commands.Account.VerifyResetToken;
using ECommerceBackend.Application.Features.Queries.Account.GetAuthState;
using ECommerceBackend.Application.Features.Queries.Account.GetUserInfo;
using ECommerceBackend.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceBackend.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController(IMediator mediator, IMailService mailService) : ControllerBase
    {
        readonly IMediator _mediator = mediator;
        readonly IMailService _mailService = mailService;

        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] RegisterUserCommandRequest registerUserCommandRequest)
        {
            RegisterUserCommandResponse response = await _mediator.Send(registerUserCommandRequest);
            return Ok(response);
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<ActionResult> Logout()
        {
            LogoutUserCommandResponse response = await _mediator.Send(new LogoutUserCommandRequest());
            return Ok(response);
        }

        [HttpGet("user-info")]
        public async Task<ActionResult> GetUserInfo()
        {
            GetUserInfoQueryResponse response = await _mediator.Send(new GetUserInfoQueryRequest());
            return Ok(response);
        }

        [HttpGet("auth-status")]
        public async Task<ActionResult> GetAuthState()
        {
            GetAuthStateQueryResponse response = await _mediator.Send(new GetAuthStateQueryRequest());
            return Ok(response);
        }


        [Authorize]
        [HttpPost("address")]
        public async Task<ActionResult<Address>> CreateOrUpdateAddress([FromBody] AddressDto addressDto)
        {
            var command = new CreateOrUpdateAddressCommandRequest { AddressDto = addressDto };
            CreateOrUpdateAddressCommandResponse response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPost("password-reset")]
        public async Task<IActionResult> PasswordReset([FromBody] PasswordResetCommandRequest passwordResetCommandRequest)
        {
            PasswordResetCommandResponse response = await _mediator.Send(passwordResetCommandRequest);
            return Ok(response);
        }

        [HttpPost("verify-reset-token")]
        public async Task<IActionResult> VerifyResetToken([FromBody] VerifyResetTokenCommandRequest verifyResetTokenCommandRequest)
        {
            VerifyResetTokenCommandResponse response = await _mediator.Send(verifyResetTokenCommandRequest);
            return Ok(response);
        }

        [HttpPost("update-password")]
        public async Task<IActionResult> UpdatePassword([FromBody] UpdatePasswordCommandRequest updatePasswordCommandRequest)
        {
            UpdatePasswordCommandResponse response = await _mediator.Send(updatePasswordCommandRequest);
            return Ok(response);
        }

    }
}