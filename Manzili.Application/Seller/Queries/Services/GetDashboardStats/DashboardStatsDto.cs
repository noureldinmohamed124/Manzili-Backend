using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manzili.Application.Seller.Queries.Services.GetDashboardStats
{
    public class DashboardStatsDto
    {
        public int TotalServices { get; set; }
        public int ActiveOrders { get; set; }
        public int CompletedOrders { get; set; }
        public decimal TotalRevenue { get; set; }
        public decimal AverageRating { get; set; }
    }
}
