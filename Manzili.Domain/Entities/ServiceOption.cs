using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manzili.Domain.Entities
{
    public class ServiceOption
    {
        public int Id { get; set; }
        public string ServiceOptionName { get; set; } = string.Empty;
        public decimal PriceAdjustment { get; set; }
        public int DisplayOrder { get; set; }

        // Audit
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // ============================
        // Navigation Properties
        // ============================

        public int OptionGroupId { get; set; }
        public ServiceOptionGroup OptionGroup { get; set; } = null!;
    }
}
