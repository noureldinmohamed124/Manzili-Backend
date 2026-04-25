using Manzili.Application.Common.Enums;

namespace Manzili.Api.DTOs.Orders
{
    public class GetOrdersRequestDto
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public OrderTransactionTypeEnum? status { get; set; }
    }
}
