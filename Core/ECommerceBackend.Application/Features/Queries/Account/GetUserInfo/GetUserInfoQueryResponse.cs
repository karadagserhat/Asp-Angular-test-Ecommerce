using ECommerceBackend.Domain.Entities;

namespace ECommerceBackend.Application.Features.Queries.Account.GetUserInfo
{
    public class GetUserInfoQueryResponse
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public Address? Address { get; set; }
    }
}
