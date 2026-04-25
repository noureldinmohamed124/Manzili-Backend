using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manzili.Application.Commands.Orders.SubmitPayment
{
    public class PaymentSuccessDto
    {
        public string OrderNo { get; set; } = string.Empty;
        public DateTime PaymentDate { get; set; }
        public decimal Total { get; set; }
    }
}
