using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceBackend.Application.Features.Commands.Account.RegisterUser
{
    public class RegisterUserCommandResponse
    {
        public bool Succeeded { get; set; }
        public string Message { get; set; }
    }
}