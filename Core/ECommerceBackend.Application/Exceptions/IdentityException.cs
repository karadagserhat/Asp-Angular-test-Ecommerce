using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceBackend.Application.Exceptions
{
    public class IdentityException : Exception
    {
        public List<string> Errors { get; }

        public IdentityException(string message, List<string> errors) : base(message)
        {
            Errors = errors ?? new List<string>();
        }
    }
}