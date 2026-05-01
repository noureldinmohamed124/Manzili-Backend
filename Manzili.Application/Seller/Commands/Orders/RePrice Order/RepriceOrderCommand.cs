using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manzili.Application.Seller.Commands.Orders.RePrice_Order
{
    public record RepriceOrderCommand(
        int OrderId,
        decimal NewPrice,
        string Reason
    );
}
