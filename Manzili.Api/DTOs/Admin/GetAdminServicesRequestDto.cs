using Manzili.Domain.Enums;

namespace Manzili.Api.DTOs.Admin
{
    public class GetAdminServicesRequestDto
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        public int? ProviderId { get; set; }
        public ServiceStatus? Status { get; set; }

        public string? Search { get; set; }
    }
}
