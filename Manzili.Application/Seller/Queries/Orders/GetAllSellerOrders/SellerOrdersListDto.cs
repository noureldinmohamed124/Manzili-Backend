using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manzili.Application.Seller.Queries.Orders.GetAllSellerOrders
{
    public class SellerOrdersListDto
    {
        public List<SellerOrderItemDto> Items { get; set; } = new List<SellerOrderItemDto>();
        public int TotalCount { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }

    public class SellerOrderItemDto
    {
        public int Id { get; set; }
        public string OrderCode { get; set; } = string.Empty;
        public string ProviderName { get; set; } = string.Empty;
        public string ServiceTitle { get; set; } = string.Empty;
        public decimal TotalPrice { get; set; }
        public decimal? ProposedPrice { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public string? ServiceImage { get; set; }
    }
}
