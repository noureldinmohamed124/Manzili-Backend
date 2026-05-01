using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manzili.Application.Seller.Queries.Orders.GetSellerOrderDetailsById
{
    public class SellerOrderDetailsDto
    {
        public int Id { get; set; }
        public string BuyerName { get; set; } = string.Empty;
        public string BuyerPhone { get; set; } = string.Empty;
        public string ServiceTitle { get; set; } = string.Empty;
        public string? ServiceImage { get; set; }
        public string? CustomRequestText { get; set; }
        public string? CustomRequestImage { get; set; }
        public decimal RawPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal? ProposedPrice { get; set; }
        public string? RePricingReason { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public List<SellerOrderOptionDto> Options { get; set; } = new List<SellerOrderOptionDto>();
    }

    public class SellerOrderOptionDto
    {
        public string OptionGroupName { get; set; } = string.Empty;
        public string OptionName { get; set; } = string.Empty;
        public decimal? Price { get; set; }
        public int Quantity { get; set; }
    }
}
