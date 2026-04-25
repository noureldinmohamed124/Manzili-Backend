using Manzili.Application.Common.Enums;
using Manzili.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manzili.Application.Common.Extensions
{
    public static class TransactionTypeExtensions
    {
        public static int ToId(this TransactionStatus status)
        => (int)status;

        public static int ToId(this OrderTransactionTypeEnum status)
        => (int)status;

        public static string OrderToString(this OrderTransactionTypeEnum status)
            => status.ToString();

        // if (status.IsTerminal())
        public static bool IsTerminal(this OrderTransactionTypeEnum status)
        {
            return status is
                OrderTransactionTypeEnum.Rejected or
                OrderTransactionTypeEnum.CancelledByBuyer or
                OrderTransactionTypeEnum.CancelledBySeller or
                OrderTransactionTypeEnum.Expired;
        }


        public static bool IsActive(this OrderTransactionTypeEnum status)
        {
            return status is
                OrderTransactionTypeEnum.Accepted or
                OrderTransactionTypeEnum.Paid or
                OrderTransactionTypeEnum.InProgress;
        }
    }
}
