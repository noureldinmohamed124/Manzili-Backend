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

        public static int ToId(this OrderStatus status)
        => (int)status;
    }
}
