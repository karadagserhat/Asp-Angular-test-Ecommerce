using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ECommerceBackend.Application.Abstractions.Services;
using ECommerceBackend.Application.DTOs;
using ECommerceBackend.Application.Exceptions;
using ECommerceBackend.Application.Helpers;
using ECommerceBackend.Domain.Entities;
using ECommerceBackend.Persistence.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;

namespace ECommerceBackend.Persistence.Services
{
    public class AccountService(IMailService mailService, SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, IHttpContextAccessor httpContextAccessor) : IAccountService
    {
        readonly SignInManager<AppUser> _signInManager = signInManager;
        readonly UserManager<AppUser> _userManager = userManager;
        readonly IMailService _mailService = mailService;

        public async Task<AddressDto> CreateOrUpdateAddress(AddressDto addressDto)
        {
            var user = await _signInManager.UserManager.GetUserByEmailWithAddress(httpContextAccessor.HttpContext.User);

            if (user.Address == null)
            {
                user.Address = addressDto.ToEntity();
            }
            else
            {
                user.Address.UpdateFromDto(addressDto);
            }

            var result = await _signInManager.UserManager.UpdateAsync(user);

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

            var user = await _signInManager.UserManager.GetUserByEmailWithAddress(httpContextAccessor.HttpContext.User);

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
            await _signInManager.SignOutAsync();
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
            var result = await _signInManager.UserManager.CreateAsync(user, registerDto.Password);

            RegisterUserResponseDTO response = new() { Succeeded = result.Succeeded };


            if (result.Succeeded)
                response.Message = "Kullanıcı başarıyla oluşturulmuştur.";
            else
                foreach (var error in result.Errors)
                    response.Message += $"{error.Code} - {error.Description}\n";

            return response;
        }

        public async Task PasswordResetAsync(string email)
        {
            AppUser? user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                string resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);

                resetToken = resetToken.UrlEncode();

                await _mailService.SendPasswordResetMailAsync(email, user.Id, resetToken);
            }
        }

        public async Task<bool> VerifyResetTokenAsync(string resetToken, string userId)
        {
            AppUser? user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                resetToken = resetToken.UrlDecode();

                return await _userManager.VerifyUserTokenAsync(user, _userManager.Options.Tokens.PasswordResetTokenProvider, "ResetPassword", resetToken);
            }
            return false;
        }

        public async Task UpdatePasswordAsync(string userId, string resetToken, string newPassword)
        {
            AppUser? user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                resetToken = resetToken.UrlDecode();

                IdentityResult result = await _userManager.ResetPasswordAsync(user, resetToken, newPassword);
                if (result.Succeeded)
                    await _userManager.UpdateSecurityStampAsync(user);
                else
                    throw new PasswordChangeFailedException();
            }
        }
    }


}