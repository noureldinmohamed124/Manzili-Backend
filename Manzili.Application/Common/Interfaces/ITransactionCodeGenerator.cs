using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manzili.Application.Common.Interfaces
{
    public interface ITransactionCodeGenerator
    {
        string GenerateOrderCode(long id);
        string GeneratePaymentCode(long id);
    }
}
