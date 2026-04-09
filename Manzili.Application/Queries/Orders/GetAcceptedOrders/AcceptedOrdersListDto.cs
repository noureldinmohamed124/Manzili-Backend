using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manzili.Application.Queries.Orders.GetAcceptedOrders
{
    public class AcceptedOrdersListDto
    {
        public IReadOnlyList<AcceptedOrderItemDto> Items { get; set; } = new List<AcceptedOrderItemDto>();
    }
    public class AcceptedOrderItemDto
    {
        public int Id { get; set; }
        public string ServiceName { get; set; } = string.Empty;
        public decimal TotalPrice { get; set; }

        public string SellerName { get; set; } = string.Empty;
    }
}
