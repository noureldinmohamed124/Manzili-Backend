using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manzili.Application.Admin.Payments.Commands
{
    public class RejectPaymentProofCommand
    {
        public int TransactionId { get; set; }

        public string RejectionReason { get; set; } = string.Empty;
    }
}
