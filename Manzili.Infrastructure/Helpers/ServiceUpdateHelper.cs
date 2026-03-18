using Manzili.Domain.Entities;
using Manzili.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Manzili.Infrastructure.Helpers
{
    public class ServiceUpdateHelper
    {
        private readonly ManziliDbContext _context;

        public ServiceUpdateHelper(ManziliDbContext context)
        {
            _context = context;
        }

        public async Task UpdateAsync(int serviceId, Expression<Func<SetPropertyCalls<Service>, SetPropertyCalls<Service>>> set)
        {
            await _context.Services
                .ExecuteUpdateAsync(set);

        }

        public async Task UpdateAsync(int serviceId, Expression<Func<Service, bool>> predicate, Expression<Func<SetPropertyCalls<Service>, SetPropertyCalls<Service>>> set)
        {
            await _context.Services
                .Where(predicate)
                .ExecuteUpdateAsync(set);
        }

    }
}