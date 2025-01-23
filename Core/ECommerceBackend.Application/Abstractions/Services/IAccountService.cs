using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerceBackend.Application.DTOs;
using ECommerceBackend.Domain.Entities;

namespace ECommerceBackend.Application.Abstractions.Services
{
    public interface IAccountService
    {
        Task<RegisterUserResponseDTO> RegisterUserAsync(RegisterDTO registerDto);
        Task LogoutUserAsync();
        Task<GetUserInfoDTO> GetUserInfoAsync();
        GetAuthStateDTO GetAuthState();
        Task<AddressDto> CreateOrUpdateAddress(AddressDto addressDto);
    }
}