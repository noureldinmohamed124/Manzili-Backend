using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manzili.Domain.Entities
{
    public class TransactionOption
    {
        public int Id { get; set; }
        public string OptionName { get; set; } = string.Empty;
        public decimal? Price { get; set; }
        public int Quantity { get; set; }

        // ============================
        // Navigation Properties
        // ============================

        public int TransactionId { get; set; }
        public Transaction Transaction { get; set; } = null!;

        public int ServiceOptionGroupId { get; set; }

        public int ServiceOptionId { get; set; }
        public ServiceOption ServiceOption { get; set; } = null!;
    }
}
