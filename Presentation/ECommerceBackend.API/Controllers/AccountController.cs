using ECommerceBackend.Application.DTOs;
using ECommerceBackend.Application.Features.Commands.Account.CreateOrUpdateAddress;
using ECommerceBackend.Application.Features.Commands.Account.LogoutUser;
using ECommerceBackend.Application.Features.Commands.Account.RegisterUser;
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
    public class AccountController(IMediator mediator) : ControllerBase
    {
        readonly IMediator _mediator = mediator;

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

        [HttpGet]
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

    }
}