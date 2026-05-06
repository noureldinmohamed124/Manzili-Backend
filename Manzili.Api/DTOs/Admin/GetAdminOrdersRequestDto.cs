using Manzili.Application.Common.Enums;

namespace Manzili.Api.DTOs.Admin
{
    public class GetAdminOrdersRequestDto
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        public OrderTransactionTypeEnum? Status { get; set; }

        public int? BuyerId { get; set; }
        public int? ProviderId { get; set; }
    }
}
