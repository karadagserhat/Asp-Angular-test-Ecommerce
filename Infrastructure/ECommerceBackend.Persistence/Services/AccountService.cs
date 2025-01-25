using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Security.Claims;
using System.Threading.Tasks;
using ECommerceBackend.Application.Abstractions.Services;
using ECommerceBackend.Application.DTOs;
using ECommerceBackend.Application.Exceptions;
using ECommerceBackend.Domain.Entities;
using ECommerceBackend.Persistence.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ECommerceBackend.Persistence.Services
{
    public class AccountService(SignInManager<AppUser> signInManager, IHttpContextAccessor httpContextAccessor) : IAccountService
    {
        public async Task<AddressDto> CreateOrUpdateAddress(AddressDto addressDto)
        {
            var user = await signInManager.UserManager.GetUserByEmailWithAddress(httpContextAccessor.HttpContext.User);

            if (user.Address == null)
            {
                user.Address = addressDto.ToEntity();
            }
            else
            {
                user.Address.UpdateFromDto(addressDto);
            }

            var result = await signInManager.UserManager.UpdateAsync(user);

            if (!result.Succeeded)
                throw new Exception("Problem updating user address");

            return user.Address.ToDto()!;
        }

        public GetAuthStateDTO GetAuthState()
        {
            var isAuthenticated = httpContextAccessor.HttpContext.User.Identity?.IsAuthenticated ?? false;

            return new GetAuthStateDTO
            {
                IsAuthenticated = isAuthenticated
            };
        }

        public async Task<GetUserInfoDTO> GetUserInfoAsync()
        {
            if (httpContextAccessor.HttpContext.User.Identity?.IsAuthenticated == false) return null!;

            var user = await signInManager.UserManager.GetUserByEmailWithAddress(httpContextAccessor.HttpContext.User);

            return new GetUserInfoDTO
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Address = user.Address,
                Roles = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Role)
            };
        }

        public async Task LogoutUserAsync()
        {
            await signInManager.SignOutAsync();
        }

        public async Task<RegisterUserResponseDTO> RegisterUserAsync(RegisterDTO registerDto)
        {
            var user = new AppUser
            {
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                Email = registerDto.Email,
                UserName = registerDto.Email
            };
            var result = await signInManager.UserManager.CreateAsync(user, registerDto.Password);

            RegisterUserResponseDTO response = new() { Succeeded = result.Succeeded };


            if (result.Succeeded)
                response.Message = "Kullanıcı başarıyla oluşturulmuştur.";
            else
                foreach (var error in result.Errors)
                    response.Message += $"{error.Code} - {error.Description}\n";

            return response;
        }
    }
}