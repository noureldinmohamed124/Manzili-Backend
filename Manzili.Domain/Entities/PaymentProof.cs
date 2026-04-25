using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manzili.Domain.Entities
{
    public class PaymentProof
    {
        public int Id { get; set; }
        public string ScreenshotUrl { get; set; } = string.Empty;
        public string? Notes { get; set; }
        public bool IsVerified { get; set; } = false;
        public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;
        public DateTime? VerifiedAt { get; set; }
        public int? VerifiedByAdminId { get; set; }

        // ============================
        // Navigation Properties
        // ============================

        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}
