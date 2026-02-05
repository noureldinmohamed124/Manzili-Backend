using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manzili.Domain.Entities
{
    public class Promotion
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string DiscountType { get; set; } = string.Empty; // (Percentage / Fixed)
        public decimal DiscountValue { get; set; }
        public decimal MinimumPurchase { get; set; }
        public int UsageLimit { get; set; }
        public string DiscountScope { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public DateTime StartDate { get; set; } = DateTime.UtcNow;
        public DateTime? EndDate { get; set; }

        // Audit
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // ============================
        // Navigation Properties
        // ============================

        public ICollection<PromotionUsage> PromotionUsages { get; set; } = new List<PromotionUsage>();

        public int ServiceId { get; set; }
        public Service Service { get; set; } = null!;

        public int ProviderId { get; set; }
        public User Provider { get; set; } = null!;

    }
}
