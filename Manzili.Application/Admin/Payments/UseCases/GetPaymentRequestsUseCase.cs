using Manzili.Application.Abstractions.Persistence;
using Manzili.Application.Admin.Payments.Queries.GetAllPaymentRequests;
using Manzili.Application.Admin.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manzili.Application.Admin.Payments.UseCases
{
    public class GetPaymentRequestsUseCase
    {
        private readonly IAdminRepo _adminRepo;

        public GetPaymentRequestsUseCase(IAdminRepo adminRepo)
        {
            _adminRepo = adminRepo;
        }

        public async Task<PagedResult<PaymentProofsRequestDto>> ExecuteAsync(GetPaymentRequestsQuery query, CancellationToken cancellationToken = default)
        {
            return await _adminRepo.GetPaymentRequestsAsync(query, cancellationToken);
        }
    }
}
