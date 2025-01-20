﻿using ECommerceBackend.Application.Repositories;
using ECommerceBackend.Domain.Entities.Common;
using ECommerceBackend.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace ECommerceBackend.Persistence.Repositories
{
    public class ReadRepository<T>(ECommerceBackendDbContext context) : IReadRepository<T> where T : BaseEntity
    {
        private readonly ECommerceBackendDbContext _context = context;

        public DbSet<T> Table => _context.Set<T>();

        public IQueryable<T> GetAll(bool tracking = true)
        {
            var query = Table.AsQueryable();
            if (!tracking) query = query.AsNoTracking();
            return query;
        }

        public async Task<T?> GetSingleAsync(System.Linq.Expressions.Expression<Func<T, bool>> method, bool tracking = true)
        {
            var query = Table.AsQueryable();
            if (!tracking) query = Table.AsNoTracking();
            return await query.FirstOrDefaultAsync(method);
        }

        public async Task<T?> GetByIdAsync(int id, bool tracking = true)
        //=> await Table.FirstOrDefaultAsync(data => data.Id == Guid.Parse(id));
        //=> await Table.FindAsync(Guid.Parse(id));
        {
            var query = Table.AsQueryable();
            if (!tracking) query = Table.AsNoTracking();
            return await query.FirstOrDefaultAsync(data => data.Id == id);
        }
    }
}
