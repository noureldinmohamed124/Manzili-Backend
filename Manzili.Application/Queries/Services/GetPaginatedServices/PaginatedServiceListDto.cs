using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manzili.Application.Queries.Services.GetPaginatedServices
{
    public class PaginatedServiceListDto
    {
        public IReadOnlyList<ServiceListItemDto> Items { get; set; } = null!;
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public bool HasMore { get; set; }

    }

    public class ServiceListItemDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string ProviderName { get; set; } = null!;
        public decimal BasePrice { get; set; }
        public double Rating { get; set; }
        public string? ImageUrl { get; set; }
    }
}
