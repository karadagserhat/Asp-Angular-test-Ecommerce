using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceBackend.Application.DTOs
{
    public class RegisterUserResponseDTO
    {
        public bool Succeeded { get; set; }
        public string Message { get; set; }
    }
}