using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manzili.Domain.Entities
{
    public class Service
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string ServiceDescription { get; set; } = string.Empty;
        public decimal BasePrice { get; set; }
        public int ViewsCount { get; set; }
        public int TotalPurchases { get; set; }
        public bool IsFeatured { get; set; }
        public bool IsRecommended { get; set; }
        public bool AutoAcceptance { get; set; }
        public bool HasActivePromotion { get; set; }
        public bool IsDeleted { get; set; }

        // Audit
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;

        // ============================
        // Navigation Properties
        // ============================

        //public ICollection<ServiceOption> ServiceOptions { get; set; } = new List<ServiceOption>();
        public ICollection<ServiceImage> ServiceImages { get; set; } = new List<ServiceImage>();
        public ICollection<Promotion> Promotions { get; set; } = new List<Promotion>();
        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
        public ICollection<ServiceOptionGroup> OptionGroups { get; set; } = new List<ServiceOptionGroup>();

        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;

        public int ProviderId { get; set; }
        public User Provider { get; set; } = null!;

        public int StatusId { get; set; }
        public Status Status { get; set; } = null!;

    }
}
