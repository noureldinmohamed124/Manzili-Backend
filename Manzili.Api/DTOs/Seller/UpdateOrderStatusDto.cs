using Manzili.Application.Common.Enums;

namespace Manzili.Api.DTOs.Seller
{
    public class UpdateOrderStatusDto
    {
        public OrderTransactionTypeEnum Status { get; set; }
    }
}
