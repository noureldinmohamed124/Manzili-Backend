using Manzili.Application.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manzili.Application.Admin.Orders.Queries.GetAdminOrders
{
    public class GetAdminOrdersQuery
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        public OrderTransactionTypeEnum? Status { get; set; }

        public int? BuyerId { get; set; }
        public int? ProviderId { get; set; }
    }
}
