using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manzili.Application.Queries.Services.GetServiceByName
{
    public class ServiceSearchDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public decimal BasePrice { get; set; }

        public string ProviderName { get; set; } = null!;
        public string? ThumbnailImageUrl { get; set; }
    }
}
