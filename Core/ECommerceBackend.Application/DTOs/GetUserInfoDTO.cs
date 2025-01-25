using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerceBackend.Domain.Entities;

namespace ECommerceBackend.Application.DTOs
{
    public class GetUserInfoDTO
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Roles { get; set; }
        public Address? Address { get; set; }
    }
}