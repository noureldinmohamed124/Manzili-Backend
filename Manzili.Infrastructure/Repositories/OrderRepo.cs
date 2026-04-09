using Manzili.Application.Abstractions.Persistence;
using Manzili.Application.Common.Extensions;
using Manzili.Application.Queries.Orders.GetAcceptedOrders;
using Manzili.Domain.Entities;
using Manzili.Domain.Enums;
using Manzili.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manzili.Infrastructure.Repositories
{
    public class OrderRepo : GenericRepo<Transaction>, IOrderRepo
    {
        public OrderRepo(ManziliDbContext context) : base(context)
        {
        }

        public async Task<AcceptedOrdersListDto> GetAcceptedOrdersForBuyerAsync(int buyerId)
        {
            var AcceptedOrdersId = TransactionStatus.Accepted.ToId();
            var AcceptedOrdersList = new AcceptedOrdersListDto();

            AcceptedOrdersList.Items = await _context.Transactions
                .Where(t => t.BuyerId == buyerId && t.TransactionTypeId == AcceptedOrdersId)
                .Select(t => new AcceptedOrderItemDto
                {
                    Id = t.Id,
                    ServiceName = t.Service!.Title,
                    TotalPrice = t.TotalPrice,
                    SellerName = t.Provider.FullName,
                    
                }).ToListAsync();

            return AcceptedOrdersList;
        }
    }
}
