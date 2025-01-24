using ECommerceBackend.Application.Repositories;
using ECommerceBackend.Domain.Entities;
using ECommerceBackend.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceBackend.Persistence.Repositories
{
    public class DeliveryMethodWriteRepository : WriteRepository<DeliveryMethod>, IDeliveryMethodWriteRepository
    {
        public DeliveryMethodWriteRepository(ECommerceBackendDbContext context) : base(context)
        {
        }
    }
}
