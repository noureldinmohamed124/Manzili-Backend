using Manzili.Application.Queries.Orders.GetAcceptedOrders;
using Manzili.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manzili.Application.Abstractions.Persistence
{
    public interface IOrderRepo : IGenericRepo<Transaction>
    {
        public Task<AcceptedOrdersListDto> GetAcceptedOrdersForBuyerAsync(int buyerId);
    }
}
