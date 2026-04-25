using Manzili.Application.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manzili.Application.Queries.Orders.GetAllOrders
{
    public record GetOrdersQuery(
        OrderTransactionTypeEnum? Status,
        int Page = 1,
        int PageSize = 10
    );
}
