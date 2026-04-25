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

        PendingPaymentVerification = 6,

        Paid = 7,
        InProgress = 8,
        ReadyForShipping = 9,
        Shipped = 10,

        Rejected = 11,
        CancelledByBuyer = 12,
        CancelledBySeller = 13,
        Expired = 14
    }
}
