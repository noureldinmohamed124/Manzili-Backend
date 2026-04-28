using Manzili.Application.Seller.Queries.Services.GetDashboardStats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manzili.Application.Abstractions.Persistence
{
    public interface ISellerRepo
    {
        Task<DashboardStatsDto> GetSellerDashboardStatsAsync(int sellerId);
    }
}
