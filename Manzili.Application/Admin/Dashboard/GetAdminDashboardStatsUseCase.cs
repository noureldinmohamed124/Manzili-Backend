using Manzili.Application.Abstractions.Persistence;
using Manzili.Application.Admin.Dashboard.Queries.GetAdminDashboardStats;
using Manzili.Application.Seller.Queries.Services.GetDashboardStats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manzili.Application.Admin.Dashboard
{
    public class GetAdminDashboardStatsUseCase
    {
        private readonly IAdminRepo _adminRepo;

        public GetAdminDashboardStatsUseCase(IAdminRepo adminRepo)
        {
            _adminRepo = adminRepo;
        }


        public async Task<AdminDashboardStatsDto> ExecuteAsync(GetDashboardStatsQuery query, CancellationToken cancellationToken = default)
        {
            return await _adminRepo.GetDashboardStatsAsync(cancellationToken);
        }
    }
}
