using Manzili.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manzili.Application.Seller.Queries.Services.GetAllSellerServices
{
    public class GetSellerServicesQuery
    {
        public ServiceStatus? Status { get; init; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;

    }
    
      
    
}
