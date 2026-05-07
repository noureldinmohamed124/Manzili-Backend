using Manzili.Application.Abstractions.Persistence;
using Manzili.Application.Abstractions.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manzili.Application.Admin.Payments.UseCases
{
    public class ApprovePaymentUseCase
    {
        private readonly IAdminRepo _adminRepo;
        private readonly ICurrentUserService _currentUserService;

        public ApprovePaymentUseCase(IAdminRepo adminRepo, ICurrentUserService currentUserService)
        {
            _adminRepo = adminRepo;
            _currentUserService = currentUserService;
        }

        public async Task ExecuteAsync(int transactionId, CancellationToken cancellationToken = default)
        {
            var adminId = _currentUserService.UserId;
            await _adminRepo.ApprovePaymentAsync(transactionId, adminId, cancellationToken);
        }
    }
}
