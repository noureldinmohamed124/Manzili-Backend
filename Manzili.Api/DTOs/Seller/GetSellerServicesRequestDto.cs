using Manzili.Domain.Enums;

namespace Manzili.Api.DTOs.Seller
{
    public class GetSellerServicesRequestDto
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public ServiceStatus? status { get; set; }
    }
}
