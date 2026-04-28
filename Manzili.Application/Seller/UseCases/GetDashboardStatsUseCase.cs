using Manzili.Application.Abstractions.Persistence;
using Manzili.Application.Abstractions.Security;
using Manzili.Application.Seller.Queries.Services.GetDashboardStats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manzili.Application.Seller.UseCases
{
    public class GetDashboardStatsUseCase
    {
        private readonly ISellerRepo _sellerRepo;
        private readonly ICurrentUserService _currentUser;

        public GetDashboardStatsUseCase(ISellerRepo sellerRepo, ICurrentUserService currentUser)
        {
            _sellerRepo = sellerRepo;
            _currentUser = currentUser;
        }

        public async Task<DashboardStatsDto> ExecuteAsync()
        {
            int sellerId = _currentUser.UserId;
            return await _sellerRepo.GetSellerDashboardStatsAsync(sellerId);
        }
    }
}
