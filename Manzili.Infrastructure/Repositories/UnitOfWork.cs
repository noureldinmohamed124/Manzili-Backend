using Manzili.Application.Abstractions.Persistence;
using Manzili.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manzili.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ManziliDbContext _context;

        public UnitOfWork(ManziliDbContext context)
        {
            _context = context;
        }

        public async Task SaveChangesAsync(CancellationToken ct = default)
        {
            await _context.SaveChangesAsync(ct);
        }
    }
}
