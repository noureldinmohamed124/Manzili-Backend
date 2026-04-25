using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manzili.Application.Commands.Orders.SubmitPayment
{
    public record SubmitPaymentCommand(
        IReadOnlyList<int> OrderIds,
        string PaymentScreenshot,
        string? Notes
    );
}
