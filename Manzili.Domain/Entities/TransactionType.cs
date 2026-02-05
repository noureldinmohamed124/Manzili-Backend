using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manzili.Domain.Entities
{
    public class TransactionType
    {
        public int Id { get; set; }
        public string TransactionTypeName { get; set; } = string.Empty;
        public int BalanceSign { get; set; }
        public int? StockSign { get; set; }

        // ============================
        // Navigation Properties
        // ============================
        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}
