using Manzili.Application.Common.Enums;
using Manzili.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manzili.Application.Common.Extensions
{
    public static class FinancialTransactionTypesExtension
    {
        public static readonly int[] RevenueStatuses =
        {
            OrderTransactionTypeEnum.Paid.ToId(),
            OrderTransactionTypeEnum.InProgress.ToId(),
            OrderTransactionTypeEnum.ReadyForShipping.ToId(),
            OrderTransactionTypeEnum.Shipped.ToId(),
            OrderTransactionTypeEnum.OutForDelivery.ToId(),
            OrderTransactionTypeEnum.Confirmed.ToId(),
            FinancialTransactionTypeEnum.EscrowPayment.ToId(),
        };

        public static int ToId(this FinancialTransactionTypeEnum status)
        => (int)status;
    }
}
