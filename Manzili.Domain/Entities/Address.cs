using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manzili.Domain.Entities
{
    public class Address
    {
        public int Id { get; set; }
        public string Label { get; set; } = string.Empty;
        public string Government { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string FullAddress { get; set; } = string.Empty;
        public int? PostalCode { get; set; }
        public string Country { get; set; } = "Egypt";
        public string? DeliveryNotes { get; set; }
        public bool IsDefualt { get; set; } = true;
        public bool IsDeleted { get; set; } = false;

        // Audit
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // ============================
        // Navigation Properties
        // ============================

        public int UserId { get; set; }
        public User User { get; set; } = null!;
    }
}
