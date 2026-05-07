using Manzili.Application.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manzili.Application.Buyer.Queries.Orders.GetAllOrders
{
    public class OrdersListDto
    {
        public IReadOnlyList<OrderItemDto> Items { get; set; } = new List<OrderItemDto>();
    }

    public class OrderItemDto
    {
        public int Id { get; set; }
        public string OrderCode { get; set; } = string.Empty;
        public string ServiceName { get; set; } = string.Empty;
        public string CustomizationDetails { get; set; } = string.Empty;
        public decimal RawOrderPrice { get; set; }
        public decimal DeliveryFees { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; } = string.Empty;
        public string ProviderName { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public List<OrderItemOptions> Options { get; set; } = new List<OrderItemOptions>();
    }
    public class OrderItemOptions
    {
        public string GroupOption { get; set; } = string.Empty;
        public string Option { get; set; } = string.Empty;
        public int Quantity { get; set; }
    }
}
