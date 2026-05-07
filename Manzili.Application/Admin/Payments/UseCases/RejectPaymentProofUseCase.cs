using Manzili.Application.Abstractions.Persistence;
using Manzili.Application.Admin.Payments.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manzili.Application.Admin.Payments.UseCases
{
    public class RejectPaymentProofUseCase
    {
        private readonly IAdminRepo _adminRepo;

        public RejectPaymentProofUseCase(IAdminRepo adminRepo)
        {
            _adminRepo = adminRepo;
        }

        public async Task ExecuteAsync(RejectPaymentProofCommand command, CancellationToken cancellationToken = default)
        {
            await _adminRepo.RejectPaymentAsync(command, cancellationToken);
        }
    }
}
