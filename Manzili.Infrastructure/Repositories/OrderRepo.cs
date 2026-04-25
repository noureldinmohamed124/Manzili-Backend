using Manzili.Application.Abstractions.Persistence;
using Manzili.Application.Common.Enums;
using Manzili.Application.Common.Extensions;
using Manzili.Application.Queries.Orders.GetAllOrders;
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

        public async Task<OrdersListDto> GetAllOrdersAsync(int buyerId, OrderTransactionTypeEnum? status)
        {
            var query = _context.Transactions
                .Where(t => t.BuyerId == buyerId);

            if (status.HasValue)
            {
                query = query.Where(t => t.TransactionTypeId == (int)status.Value);
            }

            var orders = new OrdersListDto();

            orders.Items = await query
                .OrderByDescending(t => t.CreatedAt)
                .Select(t => new OrderItemDto
                {
                    Id = t.Id,
                    ServiceName = t.Service!.Title,
                    TotalPrice = t.TotalPrice,
                    Status = t.TransactionType.TransactionTypeName,
                    ProviderName = t.Provider.FullName,
                    CreatedAt = t.CreatedAt,
                    CustomizationDetails = t.CustomRequestText,
                    Options = t.TransactionOptions.Select(to => new OrderItemOptions
                    {
                        GroupOption = to.ServiceOptionGroup.Name,
                        Option = to.OptionName,
                        Quantity = to.Quantity,
                    }).ToList()
                }).ToListAsync();

            return orders;
        }

        public async Task<List<Transaction>> GetOrdersForPaymentSummaryAsync(int buyerId, IReadOnlyList<int> orderIds)
        {
            return await _context.Transactions
                .Include(t => t.Service).ThenInclude(s => s!.ServiceImages)
                .Include(t => t.TransactionOptions)
                .Where(t => t.BuyerId == buyerId && orderIds.Contains(t.Id))
                .ToListAsync();
        }
    }
}
