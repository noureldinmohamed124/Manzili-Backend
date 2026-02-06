using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manzili.Application.Queries.Services.GetPaginatedServices
{
    public class GetServicesQuery
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int? CategoryId { get; set; }
        public bool? IsFeatured { get; set; }
        public bool? IsRecommended { get; set; }
    }
}
