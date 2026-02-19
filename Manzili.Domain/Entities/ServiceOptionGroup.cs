using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manzili.Domain.Entities
{
    public class ServiceOptionGroup
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool IsRequired { get; set; } = false;
        public bool AllowMultiple { get; set; } = false;
        public int DisplayOrder { get; set; }

        public int ServiceId { get; set; }
        public Service Service { get; set; } = null!;

        public ICollection<ServiceOption> Options { get; set; } = new List<ServiceOption>();
    }
}
