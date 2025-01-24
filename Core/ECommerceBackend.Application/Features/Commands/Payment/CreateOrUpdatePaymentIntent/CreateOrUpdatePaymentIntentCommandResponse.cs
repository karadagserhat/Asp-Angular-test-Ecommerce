using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerceBackend.Domain.Entities;

namespace ECommerceBackend.Application.Features.Commands.Payment.CreateOrUpdatePaymentIntent
{
    public class CreateOrUpdatePaymentIntentCommandResponse
    {
        public required string Id { get; set; }
        public List<CartItem> Items { get; set; } = [];
        public string? ClientSecret { get; set; }
        public string? PaymentIntentId { get; set; }
    }
}