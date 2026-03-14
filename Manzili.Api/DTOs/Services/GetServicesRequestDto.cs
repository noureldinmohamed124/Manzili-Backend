using Manzili.Application.Common.Enums;

namespace Manzili.Api.DTOs.Services
{
    public class GetServicesRequestDto
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int? CategoryId { get; set; }
        
        public ServiceFilterType? Filter { get; set; }
        public ServiceSortBy? SortBy { get; set; }
    }
}
