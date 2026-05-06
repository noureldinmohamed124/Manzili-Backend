using Manzili.Application.Abstractions.Persistence;
using Manzili.Application.Admin.Financials.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manzili.Application.Admin.Financials.UseCases
{
    public class GetAdminFinancialsUseCase
    {
        private readonly IAdminRepo _adminRepo;

        public GetAdminFinancialsUseCase(IAdminRepo adminRepo)
        {
            _adminRepo = adminRepo;
        }


        public async Task<AdminFinancialsResult> ExecuteAsync(GetAdminFinancialsQuery query, CancellationToken cancellationToken = default)
        {
            return await _adminRepo.GetFinancialsAsync(query, cancellationToken);
        }
    }
}
