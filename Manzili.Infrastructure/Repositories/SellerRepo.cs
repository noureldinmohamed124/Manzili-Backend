using Manzili.Application.Abstractions.Persistence;
using Manzili.Application.Common.Enums;
using Manzili.Application.Common.Extensions;
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

        public async Task<DashboardStatsDto> GetSellerDashboardStatsAsync(int sellerId)
        {
            var totalServices = await _context.Services
                .CountAsync(s => s.ProviderId == sellerId);

            var activeOrders = await _context.Transactions
                .CountAsync(t =>
                    t.ProviderId == sellerId &&
                    (
                        t.TransactionTypeId == OrderTransactionTypeEnum.Accepted.ToId() ||

                        t.TransactionTypeId == OrderTransactionTypeEnum.Paid.ToId() ||

                        t.TransactionTypeId == OrderTransactionTypeEnum.InProgress.ToId() ||

                        t.TransactionTypeId == OrderTransactionTypeEnum.ReadyForShipping.ToId()
                    )
                );

            var pendingRequests = await _context.Transactions
                .CountAsync(t => t.ProviderId == sellerId && t.TransactionTypeId == OrderTransactionTypeEnum.Request.ToId());

            var completedOrders = await _context.Transactions
                .CountAsync(t => t.ProviderId == sellerId && t.TransactionTypeId == (int)OrderTransactionTypeEnum.Shipped);

            var totalRevenue = await _context.Transactions
                .Where(t => t.ProviderId == sellerId && t.TransactionTypeId == (int)OrderTransactionTypeEnum.Confirmed)
                .SumAsync(t => (decimal?)t.TotalPrice) ?? 0;

            var expectedRevenue = await _context.Transactions
                .Where(t =>
                    t.ProviderId == sellerId &&
                    t.TransactionTypeId == OrderTransactionTypeEnum.Request.ToId()
                )
                .SumAsync(t => (decimal?)t.TotalPrice) ?? 0;

            var onWaitingRevenue = await _context.Transactions
                .Where(t =>
                    t.ProviderId == sellerId && 
                    (
                        t.TransactionTypeId == OrderTransactionTypeEnum.PendingPaymentVerification.ToId() ||
                        t.TransactionTypeId == OrderTransactionTypeEnum.Paid.ToId() ||
                        t.TransactionTypeId == OrderTransactionTypeEnum.InProgress.ToId() ||
                        t.TransactionTypeId == OrderTransactionTypeEnum.ReadyForShipping.ToId() ||
                        t.TransactionTypeId == OrderTransactionTypeEnum.OutForDelivery.ToId() ||
                        t.TransactionTypeId == OrderTransactionTypeEnum.Shipped.ToId() ||
                        t.TransactionTypeId == OrderTransactionTypeEnum.Delayed.ToId() ||
                        t.TransactionTypeId == OrderTransactionTypeEnum.Confirmed.ToId() ||
                        t.TransactionTypeId == OrderTransactionTypeEnum.DeliveryAttemptFailed.ToId()
                    )
                ).SumAsync(t => (decimal?)t.TotalPrice) ?? 0;

            var averageRating = await _context.Reviews
                .Where(r => r.Transaction.ProviderId == sellerId)
                .AverageAsync(r => (decimal?)r.Rating) ?? 0;

            return new DashboardStatsDto
            {
                TotalServices = totalServices,
                ActiveOrders = activeOrders,
                PendingRequests = pendingRequests,
                CompletedOrders = completedOrders,
                TotalRevenue = totalRevenue,
                ExpectedRevenue = expectedRevenue,
                OnWaitingRevenue = onWaitingRevenue,
                AverageRating = Math.Round(averageRating, 1)
            };
        }


    }
}
