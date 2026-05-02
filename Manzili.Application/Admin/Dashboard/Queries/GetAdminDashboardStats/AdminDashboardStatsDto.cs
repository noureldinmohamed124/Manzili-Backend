using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manzili.Application.Admin.Dashboard.Queries.GetAdminDashboardStats
{
    public class AdminDashboardStatsDto
    {
        public int TotalUsers { get; set; }
        public int TotalProviders { get; set; }
        public int TotalBuyers { get; set; }

        // Services
        public int TotalServices { get; set; }
        public int ActiveServices { get; set; }
        public int PendingServices { get; set; }

        // Orders
        public int TotalOrders { get; set; }
        public int ActiveOrders { get; set; }
        public int CompletedOrders { get; set; }
        public int CancelledOrders { get; set; }

        // Payments
        public int PendingPayments { get; set; }

        // Revenue
        public decimal TotalRevenue { get; set; }
    }
}
