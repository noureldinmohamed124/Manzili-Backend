using Manzili.Application.Buyer.Queries.Orders.GetAllOrders;
using Manzili.Application.Common.Enums;
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
        public Task<Transaction?> GetOrderByCode(string transactionCode);
        public Task<OrdersListDto> GetAllOrdersAsync(int buyerId, OrderTransactionTypeEnum? status);
        Task<List<Transaction>> GetOrdersForPaymentSummaryAsync(int buyerId,IReadOnlyList<int> orderIds);

        // For Submit Payment
        Task<List<Transaction>> GetOrdersForPaymentAsync(int buyerId, IReadOnlyList<int> orderIds);
    }
}
