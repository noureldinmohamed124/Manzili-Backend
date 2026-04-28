using Manzili.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manzili.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }

        // Basic Info
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        public string? ImageUrl { get; set; }

        // Auth
        public string PasswordHash { get; set; } = string.Empty;
        public UserRole Role { get; set; }

        // Blocking
        public bool IsBlocked { get; set; } = false;
        public DateTime? BlockedUntil { get; set; }
        public string? BlockReason { get; set; }

        // Audit
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;

        // ============================
        // Navigation Properties
        // ============================

        // As Provider
        public ICollection<Service> Services { get; set; } = new List<Service>();
        public ICollection<Promotion> Promotions { get; set; } = new List<Promotion>();

        // As Buyer
        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();


        // Shared
        public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
        public ICollection<Notification> Notifications { get; set; } = new List<Notification>();
        public ICollection<Address> Addresses { get; set; } = new List<Address>();
        public ICollection<PromotionUsage> PromotionUsages { get; set; } = new List<PromotionUsage>();

        public ProviderAnalytics? ProviderAnalytics { get; set; } = null!;

    }
}
