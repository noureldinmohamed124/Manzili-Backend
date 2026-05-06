using Manzili.Application.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manzili.Application.Admin.Financials.Queries
{
    public class AdminFinancialDto
    {
        public int TransactionId { get; set; }
        public int OrderId { get; set; }
        public string ServiceTitle { get; set; } = string.Empty;

        public string BuyerName { get; set; } = string.Empty;
        public string ProviderName { get; set; } = string.Empty;

        public decimal TotalPrice { get; set; }
        public OrderTransactionTypeEnum Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
