using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manzili.Application.Buyer.Queries.Services.GetServiceByName
{
    public record SearchServicesQuery(string Keyword, int PageNumber = 1, int PageSize = 10);
    
}
