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

        public static readonly int[] ActiveStatuses =
        {
            OrderTransactionTypeEnum.RePriced.ToId(),
            OrderTransactionTypeEnum.AcceptedPrice.ToId(),
            OrderTransactionTypeEnum.Accepted.ToId(),
            OrderTransactionTypeEnum.PendingPaymentVerification.ToId(),
            OrderTransactionTypeEnum.Paid.ToId(),
            OrderTransactionTypeEnum.InProgress.ToId(),
            OrderTransactionTypeEnum.ReadyForShipping.ToId(),
            OrderTransactionTypeEnum.Shipped.ToId(),
            OrderTransactionTypeEnum.OutForDelivery.ToId(),
            OrderTransactionTypeEnum.DeliveryAttemptFailed.ToId(),
            OrderTransactionTypeEnum.Delayed.ToId()
        };

        public static readonly int[] ValidOrders =
        {
            OrderTransactionTypeEnum.Cart.ToId(),
            OrderTransactionTypeEnum.Request.ToId(),
            OrderTransactionTypeEnum.RePriced.ToId(),
            OrderTransactionTypeEnum.AcceptedPrice.ToId(),
            OrderTransactionTypeEnum.Accepted.ToId(),
            OrderTransactionTypeEnum.PendingPaymentVerification.ToId(),
            OrderTransactionTypeEnum.Paid.ToId(),
            OrderTransactionTypeEnum.InProgress.ToId(),
            OrderTransactionTypeEnum.ReadyForShipping.ToId(),
            OrderTransactionTypeEnum.Shipped.ToId(),
            OrderTransactionTypeEnum.OutForDelivery.ToId(),
            OrderTransactionTypeEnum.DeliveryAttemptFailed.ToId(),
            OrderTransactionTypeEnum.Delayed.ToId()
        };
        public static bool IsTerminal(this OrderTransactionTypeEnum status)
        {
            return status == OrderTransactionTypeEnum.Rejected
                || status == OrderTransactionTypeEnum.CancelledByBuyer
                || status == OrderTransactionTypeEnum.CancelledBySeller
                || status == OrderTransactionTypeEnum.Expired;
        }

        public static bool IsActive(this OrderTransactionTypeEnum status)
        {
            return status != OrderTransactionTypeEnum.Rejected
                || status != OrderTransactionTypeEnum.CancelledByBuyer
                || status != OrderTransactionTypeEnum.CancelledBySeller
                || status != OrderTransactionTypeEnum.Expired
                || status != OrderTransactionTypeEnum.Cart;
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
