using Manzili.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manzili.Application.Admin.Services.Queries.GetAdminServices
{
    public class AdminServiceDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public decimal BasePrice { get; set; }
        public ServiceStatus Status { get; set; }
        public int ProviderId { get; set; }
        public string ProviderName { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
