using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manzili.Application.Buyer.Queries.Orders.GetPaymentSummary
{
    public record GetPaymentSummaryQuery(
        IReadOnlyList<int> OrderIds    
    );
    
}
