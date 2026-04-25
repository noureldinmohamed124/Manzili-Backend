using Manzili.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manzili.Application.Abstractions.Persistence
{
    public interface IAddressRepo : IGenericRepo<Address>
    {
        public Task<Address?> GetDefaultAddressAsync(int buyerId);
    }
}
