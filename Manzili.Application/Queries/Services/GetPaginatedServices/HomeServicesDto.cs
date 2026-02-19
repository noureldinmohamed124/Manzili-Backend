using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manzili.Application.Queries.Services.GetPaginatedServices
{
    public class HomeServicesDto
    {
        public List<ServiceListItemDto> Top_Discounts { get; set; } = new();
        public List<ServiceListItemDto> Recommended { get; set; } = new();
        public List<ServiceListItemDto> Most_Purchased { get; set; } = new();
        public List<ServiceListItemDto> Regular { get; set; } = new();
    }
}
