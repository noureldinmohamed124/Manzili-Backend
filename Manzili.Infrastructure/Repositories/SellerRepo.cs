using Manzili.Application.Abstractions.Persistence;
using Manzili.Application.Common.Enums;
using Manzili.Application.Seller.Queries.Services.GetDashboardStats;
using Manzili.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manzili.Infrastructure.Repositories
{
    public class SellerRepo : ISellerRepo
    {
        private readonly ManziliDbContext _context;

        public SellerRepo(ManziliDbContext context)
        {
            _context = context;
        }

        public async Task<DashboardStatsDto>GetSellerDashboardStatsAsync(int sellerId)
        {
            var totalServices = await _context.Services
                .CountAsync(s => s.ProviderId == sellerId);

            var activeOrders = await _context.Transactions
                .CountAsync(t =>
                    t.ProviderId == sellerId &&
                    (
                        t.TransactionTypeId ==
                            (int)OrderTransactionTypeEnum.Accepted ||

                        t.TransactionTypeId ==
                            (int)OrderTransactionTypeEnum.Paid ||

                        t.TransactionTypeId ==
                            (int)OrderTransactionTypeEnum.InProgress ||

                        t.TransactionTypeId ==
                            (int)OrderTransactionTypeEnum.ReadyForShipping
                    )
                );

            var completedOrders = await _context.Transactions
                .CountAsync(t => t.ProviderId == sellerId && t.TransactionTypeId == (int)OrderTransactionTypeEnum.Shipped);

            var totalRevenue = await _context.Transactions
                .Where(t => t.ProviderId == sellerId && t.TransactionTypeId == (int)OrderTransactionTypeEnum.Shipped)
                .SumAsync(t => (decimal?)t.TotalPrice) ?? 0;

            var averageRating = await _context.Reviews
                .Where(r => r.Transaction.ProviderId == sellerId)
                .AverageAsync(r => (decimal?)r.Rating) ?? 0;

            return new DashboardStatsDto
            {
                TotalServices = totalServices,
                ActiveOrders = activeOrders,
                CompletedOrders = completedOrders,
                TotalRevenue = totalRevenue,
                AverageRating = Math.Round(averageRating, 1)
            };
        }
    }
}
