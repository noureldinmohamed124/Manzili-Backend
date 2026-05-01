using Manzili.Application.Common.Enums;

namespace Manzili.Api.DTOs.Seller
{
    public class GetAllSellerOrdersDto
    {
        public OrderTransactionTypeEnum? Status { get; set; }
        public int? Page {  get; set; }
        public int? PageSize {  get; set; }
    }
}
