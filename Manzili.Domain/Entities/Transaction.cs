using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manzili.Domain.Entities
{
    public class Transaction
    {
        public int Id { get; set; }
        public string TransactionCode { get; set; } = string.Empty;
        public string CustomRequestText { get; set; } = string.Empty;
        public string? CustomRequestImage { get; set; }
        public decimal RawPrice { get; set; }
        public decimal CashDiscount { get; set; }
        public decimal TotalPrice { get; set; }


        // Repricing
        public decimal? ProposedPrice { get; set; }
        public string? RePricingReason { get; set; }

        // Rejection
        public string? RejectionReason { get; set; }

        // Audit
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // ============================
        // Navigation Properties
        // ============================

        public ICollection<TransactionOption> TransactionOptions { get; set; } = new List<TransactionOption>();
        public Review? Review { get; set; }

        public int BuyerId { get; set; }
        public User Buyer { get; set; } = null!;

        public int ProviderId { get; set; }
        public User Provider { get; set; } = null!;

        public int TransactionTypeId { get; set; }
        public TransactionType TransactionType { get; set; } = null!;

        // Relation with Service (Optional)
        public int? ServiceId { get; set; }
        public Service? Service { get; set; }

        // Relation on itself
        public int? ParentTransactionId { get; set; }
        public Transaction? ParentTransaction { get; set; }
    }
}
