using Manzili.Application.Abstractions.Persistence;
using Manzili.Application.Admin.Dashboard.Queries.GetAdminDashboardStats;
using Manzili.Application.Common.Enums;
using Manzili.Application.Common.Extensions;
using Manzili.Application.Seller.Queries.Services.GetDashboardStats;
using Manzili.Domain.Enums;
using Manzili.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Manzili.Infrastructure.Repositories
{
    public class AdminRepo : IAdminRepo
    {
        private readonly ManziliDbContext _context;

        public AdminRepo(ManziliDbContext context)
        {
            _context = context;
        }

        public async Task<AdminDashboardStatsDto> GetDashboardStatsAsync(CancellationToken cancellationToken = default)
        {
            // =========================
            // Users
            // =========================

            var totalUsers = await _context.Users
                .AsNoTracking()
                .CountAsync(cancellationToken);

            var totalProviders = await _context.Users
                .AsNoTracking()
                .CountAsync(
                    x => x.Role == UserRole.Provider,
                    cancellationToken);

            var totalBuyers = await _context.Users
                .AsNoTracking()
                .CountAsync(
                    x => x.Role == UserRole.Buyer,
                    cancellationToken);

            // =========================
            // Services
            // =========================

            var totalServices = await _context.Services
                .AsNoTracking()
                .CountAsync(cancellationToken);

            var activeServices = await _context.Services
                .AsNoTracking()
                .CountAsync(
                    x => x.Status.Name == ServiceStatus.Active.ToString(),
                    cancellationToken);

            var pendingServices = await _context.Services
                .AsNoTracking()
                .CountAsync(
                    x => x.Status.Name == ServiceStatus.Draft.ToString(),
                    cancellationToken);

            // =========================
            // Orders
            // =========================

            var totalOrders = await _context.Transactions
                .Where(t => t.ServiceId != null)
                .AsNoTracking()
                .CountAsync(cancellationToken);

            var activeOrders = await _context.Transactions
                .AsNoTracking()
                .CountAsync(
                t => t.ServiceId != null &&
                OrderTransactionTypeExtensions.ActiveStatuses.Contains(t.TransactionTypeId),
                cancellationToken);

            var completedOrders = await _context.Transactions
                .AsNoTracking()
                .CountAsync(
                    t => t.ServiceId == null &&
                    t.TransactionTypeId == OrderTransactionTypeEnum.Confirmed.ToId(),
                    cancellationToken);

            var cancelledOrders = await _context.Transactions
                .AsNoTracking()
                .CountAsync(
                    t => t.ServiceId == null && 
                    t.TransactionTypeId == OrderTransactionTypeEnum.CancelledByBuyer.ToId() ||
                    t.TransactionTypeId == OrderTransactionTypeEnum.CancelledBySeller.ToId(),
                    cancellationToken);

            // =========================
            // Payments
            // =========================

            var pendingPayments = await _context.Transactions
                .AsNoTracking()
                .CountAsync(
                    x => x.ServiceId != null,
                    cancellationToken);

            // =========================
            // Revenue
            // =========================

            var totalRevenue = await _context.Transactions
                .AsNoTracking()
                .Where(x => x.ServiceId == null && x.TransactionTypeId == OrderTransactionTypeEnum.Confirmed.ToId())
                .Select(x => (decimal?)x.TotalPrice)
                .SumAsync(cancellationToken) ?? 0;

            // =========================
            // Return
            // =========================

            return new AdminDashboardStatsDto
            {
                TotalUsers = totalUsers,
                TotalProviders = totalProviders,
                TotalBuyers = totalBuyers,

                TotalServices = totalServices,
                ActiveServices = activeServices,
                PendingServices = pendingServices,

                TotalOrders = totalOrders,
                ActiveOrders = activeOrders,
                CompletedOrders = completedOrders,
                CancelledOrders = cancelledOrders,

                PendingPayments = pendingPayments,

                TotalRevenue = totalRevenue
            };
        }
    }
}
