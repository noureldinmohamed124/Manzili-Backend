using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manzili.Application.Admin.Payments.Queries.GetAllPaymentRequests
{
    public class PaymentProofsRequestDto
    {
        public int TransactionId { get; set; }

        public int OrderId { get; set; }

        public string ServiceTitle { get; set; } = string.Empty;

        public string BuyerName { get; set; } = string.Empty;
        public string ProviderName { get; set; } = string.Empty;

        public decimal TotalPrice { get; set; }

        public bool IsVerified { get; set; }

        public string? PaymentProofImage { get; set; }
        public int PaymentProofId { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
