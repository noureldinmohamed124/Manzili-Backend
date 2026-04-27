using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manzili.Application.Buyer.Queries.Orders.GetPaymentSummary
{
    public class PaymentSummaryDto
    {
        public List<PaymentSummaryServiceItemDto> Services { get; set; } = [];
        public PaymentSummaryAddressDto Address { get; set; } = null!;
        public PaymentBreakdownDto PriceBreakdown { get; set; } = null!;
    }

    public class PaymentSummaryServiceItemDto
    {
        public int OrderId { get; set; }
        public string Image { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public List<PaymentSummaryServiceOptionDto> Options { get; set; } = new List<PaymentSummaryServiceOptionDto>();
    }
    public class PaymentSummaryServiceOptionDto
    {
        public string Name { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
    public class PaymentSummaryAddressDto
    {
        public string AddressPreview { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
    }
    public class PaymentBreakdownDto
    {
        public decimal Subtotal { get; set; }
        public decimal DeliveryFees { get; set; }
        public decimal Total { get; set; }
    }

}
