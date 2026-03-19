using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manzili.Domain.Enums
{
    public enum OrderStatus
    {
        Request = 1,
        RePriced = 2,
        AcceptedPrice = 3,
        Accepted = 4,
        InvoiceIssued = 5,
        Paid = 6,
        InProgress = 7,
        ReadyForShipping = 8,
        Confirmed = 9,
        // optional 
        Shipped = 10,

        // Terminal
        Rejected = 11,
        CancelledByBuyer = 12,
        CancelledBySeller = 13,
        Expired = 14,
    }
}
