using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manzili.Application.Seller.Commands.Orders.RejectOrder
{
    public record RejectOrderCommand(
        int OrderId,
        string Reason
    );
}
