using Manzili.Application.Abstractions.Persistence;
using Manzili.Application.Admin.Payments.Queries.GetPaymentProofById;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manzili.Application.Admin.Payments.UseCases
{
    public class GetPaymentProofDetailsUseCase
    {
        private readonly IAdminRepo _adminRepo;

        public GetPaymentProofDetailsUseCase(IAdminRepo adminRepo)
        {
            _adminRepo = adminRepo;
        }

        public async Task<PaymentProofDetailsDto> ExecuteAsync(int transactionId, CancellationToken cancellationToken = default)
        {
            return await _adminRepo.GetPaymentProofDetailsAsync(transactionId, cancellationToken);
        }
    }
}
