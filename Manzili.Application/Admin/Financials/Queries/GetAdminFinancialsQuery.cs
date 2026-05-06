using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manzili.Application.Admin.Financials.Queries
{
    public class GetAdminFinancialsQuery
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        public DateTime? From { get; set; }
        public DateTime? To { get; set; }

        public int? BuyerId { get; set; }
        public int? ProviderId { get; set; }
    }
}
