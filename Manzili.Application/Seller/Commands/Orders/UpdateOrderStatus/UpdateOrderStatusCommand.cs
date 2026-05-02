using Manzili.Application.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manzili.Application.Seller.Commands.Orders.UpdateOrderStatus
{
    public record UpdateOrderStatusCommand(
        int OrderId,
        OrderTransactionTypeEnum Status
    );
}
