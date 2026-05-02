using Manzili.Application.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manzili.Application.Common.Extensions
{
    public static class OrderTransactionTypeExtensions
    {
        public static bool IsTerminal(this OrderTransactionTypeEnum status)
        {
            return status == OrderTransactionTypeEnum.Rejected
                || status == OrderTransactionTypeEnum.CancelledByBuyer
                || status == OrderTransactionTypeEnum.CancelledBySeller
                || status == OrderTransactionTypeEnum.Expired;
        }

        public static bool CanBeRejected(this OrderTransactionTypeEnum status)
        {
            return status == OrderTransactionTypeEnum.Request
                ||
                status == OrderTransactionTypeEnum.RePriced;
        }


        public static bool CanBeRepriced(this OrderTransactionTypeEnum status)
        {
            return status == OrderTransactionTypeEnum.Request;
        }

        public static bool CanTransitionTo(this OrderTransactionTypeEnum current, OrderTransactionTypeEnum next)
        {
            return current switch
            {
                OrderTransactionTypeEnum.Paid => next == OrderTransactionTypeEnum.InProgress,

                OrderTransactionTypeEnum.InProgress => next == OrderTransactionTypeEnum.ReadyForShipping,

                OrderTransactionTypeEnum.ReadyForShipping => next == OrderTransactionTypeEnum.Shipped,

                _ => false
            };
        }
    }
}
