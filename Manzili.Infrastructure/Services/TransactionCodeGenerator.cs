using Manzili.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manzili.Infrastructure.Services
{
    public class TransactionCodeGenerator : ITransactionCodeGenerator
    {
        public string GenerateOrderCode(long id)
        {
            return $"ORD-{DateTime.UtcNow.Year}-{id:D6}";
        }

        public string GeneratePaymentCode(long id)
        {
            return $"PAY-{DateTime.UtcNow.Year}-{id:D6}";
        }
    }
}
