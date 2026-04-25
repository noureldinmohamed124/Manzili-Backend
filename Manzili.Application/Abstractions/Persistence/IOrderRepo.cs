using Manzili.Application.Common.Enums;
using Manzili.Application.Queries.Orders.GetAllOrders;
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
        public Task<OrdersListDto> GetAllOrdersAsync(int buyerId, OrderTransactionTypeEnum? status);
        Task<List<Transaction>> GetOrdersForPaymentSummaryAsync(int buyerId,IReadOnlyList<int> orderIds);
    }
}
