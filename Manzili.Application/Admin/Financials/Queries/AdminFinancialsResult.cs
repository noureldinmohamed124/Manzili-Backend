using Manzili.Application.Admin.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manzili.Application.Admin.Financials.Queries
{
    public class AdminFinancialsResult : PagedResult<AdminFinancialDto>
    {
        public decimal TotalRevenue { get; set; }
    }
}
