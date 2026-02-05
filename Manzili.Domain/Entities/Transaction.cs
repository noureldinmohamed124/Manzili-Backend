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
        public string CustomRequestText { get; set; } = string.Empty;
        public string? CustomRequestImage { get; set; }
        public decimal RawPrice { get; set; }
        public decimal CashDiscount { get; set; }
        public decimal TotalPrice { get; set; }

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

        // Relation on itself
        public int? ParentTransactionId { get; set; }
        public Transaction? ParentTransaction { get; set; }
    }
}
