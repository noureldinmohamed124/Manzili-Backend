using Manzili.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manzili.Application.Admin.Services.Queries.GetAdminServices
{
    public class GetAdminServicesQuery
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        public int? ProviderId { get; set; }
        public ServiceStatus? Status { get; set; }

        public string? Search { get; set; }
    }
}
