using Manzili.Application.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manzili.Application.Admin.Orders.Queries.GetAdminOrders
{
    public class AdminOrderDto
    {
        public int OrderId { get; set; }

        public int ServiceId { get; set; }
        public string ServiceTitle { get; set; } = string.Empty;

        public int BuyerId { get; set; }
        public string BuyerName { get; set; } = string.Empty;

        public int ProviderId { get; set; }
        public string ProviderName { get; set; } = string.Empty;

        public decimal TotalPrice { get; set; }

        public OrderTransactionTypeEnum CurrentStatus { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
