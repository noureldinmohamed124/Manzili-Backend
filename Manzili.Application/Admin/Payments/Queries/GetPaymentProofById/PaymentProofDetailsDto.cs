using Manzili.Application.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manzili.Application.Admin.Payments.Queries.GetPaymentProofById
{
    public class PaymentProofDetailsDto
    {// =========================
     // Order
     // =========================

        public int TransactionId { get; set; }

        public string TransactionCode { get; set; } = string.Empty;

        public OrderTransactionTypeEnum Status { get; set; }

        public decimal RawPrice { get; set; }

        public decimal CashDiscount { get; set; }

        public decimal DeliveryFees { get; set; }

        public decimal TotalPrice { get; set; }

        public DateTime CreatedAt { get; set; }

        // =========================
        // Service
        // =========================

        public int? ServiceId { get; set; }

        public string? ServiceTitle { get; set; }

        // =========================
        // Buyer
        // =========================

        public int BuyerId { get; set; }

        public string BuyerName { get; set; } = string.Empty;

        public string? BuyerPhoneNumber { get; set; }

        // =========================
        // Provider
        // =========================

        public int ProviderId { get; set; }

        public string ProviderName { get; set; } = string.Empty;

        public string? ProviderPhoneNumber { get; set; }

        // =========================
        // Payment Proof
        // =========================

        public int PaymentProofId { get; set; }

        public string PaymentProofImageUrl { get; set; } = string.Empty;

        public bool IsVerified { get; set; }

        public DateTime? VerifiedAt { get; set; }

        public int? VerifiedByAdminId { get; set; }

        //public bool IsRejected { get; set; }

        //public string? RejectionReason { get; set; }

        //public DateTime? RejectedAt { get; set; }

        //public int? RejectedByAdminId { get; set; }

        // =========================
        // Financial State
        // =========================

        public bool EscrowTransactionExists { get; set; }
    }
}
