using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manzili.Domain.Enums
{
    public enum TransactionStatus
    {
        EscrowPayment = 15,
        PlatformCommission = 16,
        SellerPayout = 17,
        RefundedEscrowPayment = 18
    }
}
