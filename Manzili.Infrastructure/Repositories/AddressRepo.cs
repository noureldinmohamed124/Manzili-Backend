using Manzili.Application.Abstractions.Persistence;
using Manzili.Domain.Entities;
using Manzili.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manzili.Infrastructure.Repositories
{
    public class AddressRepo : GenericRepo<Address>, IAddressRepo
    {
        public AddressRepo(ManziliDbContext context) : base(context)
        {
        }

        public async Task<Address?> GetByBuyerIdAsync(int buyerId)
        {
            return await _context.Addresses
                .Where(a => a.UserId == buyerId)
                .FirstOrDefaultAsync();
        }

        public async Task<Address?> GetDefaultAddressAsync(int buyerId)
        {
            return await _context.Addresses
                .Include(a => a.User)
                .FirstOrDefaultAsync(a => a.UserId == buyerId && a.IsDefualt);
        }
    }
}
