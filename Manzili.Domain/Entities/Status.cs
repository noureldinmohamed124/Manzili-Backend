using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manzili.Domain.Entities
{
    public class Status
    {
        public int Id { get; set; }
        //public string Code { get; set; } = null!;
        public string Name { get; set; } = string.Empty;
        public bool IsActive { get; set; }

        // Audit
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // ============================
        // Navigation Properties
        // ============================

        public ICollection<Service> Services { get; set; } = new List<Service>();
    }
}
