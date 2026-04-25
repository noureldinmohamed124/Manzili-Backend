using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manzili.Application.Common.Enums
{
    public enum OrderTransactionTypeEnum
    {
        Cart = 1,
        Request = 2,
        RePriced = 3,
        AcceptedPrice = 4,
        Accepted = 5,
        Paid = 6,
        InProgress = 7,
        ReadyForShipping = 8,
        Shipped = 9,

        Rejected = 10,
        CancelledByBuyer = 11,
        CancelledBySeller = 12,
        Expired = 13
    }
}
