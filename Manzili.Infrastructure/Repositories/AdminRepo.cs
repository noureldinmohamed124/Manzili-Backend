using Manzili.Application.Abstractions.Persistence;
using Manzili.Application.Admin.Dashboard.Queries.GetAdminDashboardStats;
using Manzili.Application.Admin.Financials.Queries;
using Manzili.Application.Admin.Orders.Queries.GetAdminOrders;
using Manzili.Application.Admin.Payments.Queries.GetAllPaymentRequests;
using Manzili.Application.Admin.Services.Queries.GetAdminServices;
using Manzili.Application.Admin.Users;
using Manzili.Application.Admin.Users.Queries.GetAdminAllUsers;
using Manzili.Application.Admin.Users.Queries.GetAdminUserById;
using Manzili.Application.Common.Enums;
using Manzili.Application.Common.Extensions;
using Manzili.Application.Exceptions;
using Manzili.Application.Seller.Queries.Services.GetDashboardStats;
using Manzili.Domain.Entities;
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

        public async Task<PagedResult<AdminUserDto>> GetAdminAllUsersAsync(GetAdminAllUsersQuery query, CancellationToken cancellationToken = default)
        {
            var usersQuery = _context.Users
                .Where(u => u.Role != UserRole.Admin)
                .AsNoTracking().AsQueryable();

            // =========================
            // Filtering
            // =========================

            if (query.Role.HasValue && query.Role.Value != UserRole.Admin)
            {
                usersQuery = usersQuery
                    .Where(u => u.Role == query.Role.Value);
            }

            if (query.IsBlocked.HasValue)
            {
                usersQuery = usersQuery
                    .Where(u => u.IsBlocked == query.IsBlocked.Value);
            }

            // =========================
            // Search
            // =========================

            if (!string.IsNullOrWhiteSpace(query.Search))
            {
                var search = query.Search.ToLower();

                usersQuery = usersQuery
                    .Where(u => EF.Functions.Like(u.FullName, $"%{search}%") || EF.Functions.Like(u.Email, $"%{search}%"));
                // u.FullName.ToLower().Contains(search) || u.Email.ToLower().Contains(search)
            }

            // =========================
            // Total Count
            // =========================

            var totalCount = await usersQuery
                .CountAsync(cancellationToken);

            // =========================
            // Pagination
            // =========================

            var pageSize = Math.Min(query.PageSize, 50);
            var skip = (query.Page - 1) * pageSize;

            var users = await usersQuery
                .OrderByDescending(u => u.CreatedAt)
                .Skip(skip)
                .Take(pageSize)
                .Select(u => new AdminUserDto
                {
                    Id = u.Id,
                    FullName = u.FullName,
                    ProfilePicture = u.ImageUrl ?? "",
                    Email = u.Email,
                    Role = u.Role,
                    IsBlocked = u.IsBlocked,
                    BlockedUntil = u.BlockedUntil,
                    CreatedAt = u.CreatedAt
                })
                .ToListAsync(cancellationToken);

            // =========================
            // Return
            // =========================

            return new PagedResult<AdminUserDto>
            {
                Items = users,
                TotalCount = totalCount,
                Page = query.Page,
                PageSize = pageSize
            };
        }

        public async Task<AdminUserDetailsDto?> GetUserDetailsByIdAsync(int userId, CancellationToken cancellationToken = default)
        {
            var user = await _context.Users
                .AsNoTracking()
                .Where(u => u.Id == userId)
                .Select(u => new AdminUserDetailsDto
                {
                    Id = u.Id,
                    FullName = u.FullName,
                    Email = u.Email,
                    PhoneNumber = u.PhoneNumber,
                    ImageUrl = u.ImageUrl,

                    Role = u.Role,

                    IsBlocked = u.IsBlocked,
                    BlockedUntil = u.BlockedUntil,
                    BlockReason = u.BlockReason,
                    BlockedByAdmin = u.BlockedByAdminId != null ? 
                        _context.Users.FirstOrDefault(x => x.Id == u.BlockedByAdminId)!.FullName : null,



                    CreatedAt = u.CreatedAt,
                    UpdatedAt = u.UpdatedAt,

                    // =========================
                    // Stats
                    // =========================

                    ServicesCount = _context.Services
                        .Count(s => s.ProviderId == u.Id),

                    OrdersCount = _context.Transactions
                        .Count(t => t.BuyerId == u.Id || t.ProviderId == u.Id)
                })
                .FirstOrDefaultAsync(cancellationToken);
            
            return user;
        }

        public async Task<PagedResult<AdminServiceDto>> GetServicesAsync(GetAdminServicesQuery query, CancellationToken cancellationToken = default)
        {
            var servicesQuery = _context.Services
            .AsNoTracking()
            .AsQueryable();

            // =========================
            // Filtering
            // =========================

            if (query.ProviderId.HasValue)
            {
                servicesQuery = servicesQuery
                    .Where(s => s.ProviderId == query.ProviderId.Value);
            }

            if (query.Status.HasValue)
            {
                servicesQuery = servicesQuery
                    .Where(s => s.Status.Name == query.Status.Value.ToString());
            }

            // =========================
            // Search
            // =========================

            if (!string.IsNullOrWhiteSpace(query.Search))
            {
                var search = query.Search.Trim();

                servicesQuery = servicesQuery.Where(s =>
                    EF.Functions.Like(s.Title, $"%{search}%"));
            }

            // =========================
            // Total Count
            // =========================

            var totalCount = await servicesQuery
                .CountAsync(cancellationToken);

            // =========================
            // Pagination
            // =========================

            var pageSize = Math.Min(query.PageSize, 50);
            var skip = (query.Page - 1) * pageSize;

            var items = await servicesQuery
                .OrderByDescending(s => s.CreatedAt)
                .Skip(skip)
                .Take(pageSize)
                .Select(s => new AdminServiceDto
                {
                    Id = s.Id,
                    Title = s.Title,
                    BasePrice = s.BasePrice,
                    Status = ServiceStatusSwithcher(s.Status.Name),
                    ProviderId = s.ProviderId,
                    ProviderName = s.Provider.FullName,

                    CreatedAt = s.CreatedAt
                })
                .ToListAsync(cancellationToken);

            return new PagedResult<AdminServiceDto>
            {
                Items = items,
                TotalCount = totalCount,
                Page = query.Page,
                PageSize = pageSize
            };
        }

        public async Task<PagedResult<AdminOrderDto>> GetOrdersAsync(GetAdminOrdersQuery query, CancellationToken cancellationToken = default)
        {
            // =========================
            // Step 1: Get latest transaction IDs per order
            // =========================

            var latestTransactionIds = await _context.Transactions
                .AsNoTracking()
                .Where(t => t.ServiceId != null)
                .GroupBy(t => t.Id) // ✅ FIXED
                .Select(g => g
                    .OrderByDescending(t => t.CreatedAt)
                    .Select(t => t.ParentTransactionId ?? t.Id)
                    .FirstOrDefault())
                .ToListAsync(cancellationToken);

            // =========================
            // Step 2: Base query
            // =========================

            var queryable = _context.Transactions
                .AsNoTracking()
                .Where(t => latestTransactionIds.Contains(t.Id));

            // =========================
            // Filtering
            // =========================

            if (query.Status.HasValue)
            {
                queryable = queryable
                    .Where(t => t.TransactionTypeId == query.Status.Value.ToId());
            }

            if (query.BuyerId.HasValue)
            {
                queryable = queryable
                    .Where(t => t.BuyerId == query.BuyerId.Value);
            }

            if (query.ProviderId.HasValue)
            {
                queryable = queryable
                    .Where(t => t.ProviderId == query.ProviderId.Value);
            }

            // =========================
            // Count
            // =========================

            var totalCount = await queryable
                .CountAsync(cancellationToken);

            // =========================
            // Pagination
            // =========================

            var pageSize = Math.Min(query.PageSize, 50);
            var skip = (query.Page - 1) * pageSize;

            // =========================
            // Projection (NOW SAFE)
            // =========================

            var items = await queryable
                .OrderByDescending(t => t.CreatedAt)
                .Skip(skip)
                .Take(pageSize)
                .Select(t => new AdminOrderDto
                {
                    OrderId = t.ParentTransactionId ?? t.Id, // ✅ FIXED (not t.Id)

                    ServiceId = t.ServiceId!.Value,
                    ServiceTitle = t.Service!.Title,

                    BuyerId = t.BuyerId,
                    BuyerName = t.Buyer.FullName,

                    ProviderId = t.ProviderId,
                    ProviderName = t.Provider.FullName,

                    TotalPrice = t.TotalPrice,

                    CurrentStatus = (OrderTransactionTypeEnum)t.TransactionTypeId,

                    CreatedAt = t.CreatedAt
                })
                .ToListAsync(cancellationToken);

            // =========================
            // Return
            // =========================

            return new PagedResult<AdminOrderDto>
            {
                Items = items,
                TotalCount = totalCount,
                Page = query.Page,
                PageSize = pageSize
            };
        }
        
        public async Task<AdminFinancialsResult> GetFinancialsAsync(GetAdminFinancialsQuery query, CancellationToken cancellationToken = default)
        {
            // =========================
            // Step 1: Latest transactions per order
            // =========================

            var latestTransactionIds = await _context.Transactions
                .AsNoTracking()
                .Where(t => t.ServiceId != null)
                .GroupBy(t => t.ParentTransactionId ?? t.Id)
                .Select(g => g
                    .OrderByDescending(t => t.CreatedAt)
                    .Select(t => t.Id)
                    .FirstOrDefault())
                .ToListAsync(cancellationToken);

            // =========================
            // Step 2: Base query
            // =========================

            var queryable = _context.Transactions
                .AsNoTracking()
                .Where(t => latestTransactionIds.Contains(t.Id));

            // =========================
            // Step 3: Financial filter
            // =========================

            queryable = queryable.Where(t =>
                FinancialTransactionTypesExtension.RevenueStatuses
                    .Contains(t.TransactionTypeId));

            // =========================
            // Step 4: Apply filters
            // =========================

            if (query.From.HasValue)
            {
                queryable = queryable
                    .Where(t => t.CreatedAt >= query.From.Value);
            }

            if (query.To.HasValue)
            {
                queryable = queryable
                    .Where(t => t.CreatedAt <= query.To.Value);
            }

            if (query.BuyerId.HasValue)
            {
                queryable = queryable
                    .Where(t => t.BuyerId == query.BuyerId.Value);
            }

            if (query.ProviderId.HasValue)
            {
                queryable = queryable
                    .Where(t => t.ProviderId == query.ProviderId.Value);
            }

            // =========================
            // Step 5: Total Revenue
            // =========================

            var totalRevenue = await queryable
                .SumAsync(t => (decimal?)t.TotalPrice, cancellationToken) ?? 0;

            // =========================
            // Step 6: Count
            // =========================

            var totalCount = await queryable
                .CountAsync(cancellationToken);

            // =========================
            // Step 7: Pagination
            // =========================

            var pageSize = Math.Min(query.PageSize, 50);
            var skip = (query.Page - 1) * pageSize;

            // =========================
            // Step 8: Projection
            // =========================

            var items = await queryable
                .OrderByDescending(t => t.CreatedAt)
                .Skip(skip)
                .Take(pageSize)
                .Select(t => new AdminFinancialDto
                {
                    TransactionId = t.Id,

                    OrderId = t.ParentTransactionId ?? t.Id,

                    ServiceTitle = t.Service!.Title,

                    BuyerName = t.Buyer.FullName,
                    ProviderName = t.Provider.FullName,

                    TotalPrice = t.TotalPrice,

                    Status = (FinancialTransactionTypeEnum)t.TransactionTypeId,

                    CreatedAt = t.CreatedAt
                })
                .ToListAsync(cancellationToken);

            // =========================
            // Step 9: Return
            // =========================

            return new AdminFinancialsResult
            {
                Items = items,
                TotalCount = totalCount,
                Page = query.Page,
                PageSize = pageSize,
                TotalRevenue = totalRevenue
            };
        }

        public async Task<PagedResult<PaymentProofsRequestDto>> GetPaymentRequestsAsync(GetPaymentRequestsQuery query, CancellationToken cancellationToken = default)
        {
            // =========================
            // Base Query
            // =========================

            var queryable = _context.Transactions
                .AsNoTracking()
                .Where(t =>
                    t.TransactionTypeId == OrderTransactionTypeEnum.PendingPaymentVerification.ToId() 
                    || t.TransactionTypeId == OrderTransactionTypeEnum.Paid.ToId()
                    &&
                    t.PaymentProofId != null &&
                    t.ServiceId != null);

            // =========================
            // Filtering
            // =========================

            if (query.BuyerId.HasValue)
            {
                queryable = queryable
                    .Where(t => t.BuyerId == query.BuyerId.Value);
            }

            if (query.ProviderId.HasValue)
            {
                queryable = queryable
                    .Where(t => t.ProviderId == query.ProviderId.Value);
            }

            if (query.From.HasValue)
            {
                queryable = queryable
                    .Where(t => t.CreatedAt >= query.From.Value);
            }

            if (query.To.HasValue)
            {
                queryable = queryable
                    .Where(t => t.CreatedAt <= query.To.Value);
            }

            if (query.IsVerified.HasValue)
            {
                queryable = queryable
                    .Where(t => t.PaymentProof!.IsVerified == query.IsVerified.Value);
            }

            // =========================
            // Count
            // =========================

            var totalCount = await queryable.CountAsync(cancellationToken);

            // =========================
            // Pagination
            // =========================

            var pageSize = Math.Min(query.PageSize, 50);
            var skip = (query.Page - 1) * pageSize;

            // =========================
            // Projection
            // =========================

            var items = await queryable
                .OrderByDescending(t => t.CreatedAt)
                .Skip(skip)
                .Take(pageSize)
                .Select(t => new PaymentProofsRequestDto
                {
                    TransactionId = t.Id,

                    OrderId = t.ParentTransactionId ?? t.Id,

                    ServiceTitle = t.Service!.Title,

                    BuyerName = t.Buyer.FullName,
                    ProviderName = t.Provider.FullName,

                    TotalPrice = t.TotalPrice + t.DeliveryFees,

                    IsVerified = t.PaymentProof!.IsVerified,

                    PaymentProofId = t.PaymentProofId!.Value,
                    PaymentProofImage = t.PaymentProof!.ScreenshotUrl, // adjust property name if different

                    CreatedAt = t.CreatedAt
                })
                .ToListAsync(cancellationToken);

            return new PagedResult<PaymentProofsRequestDto>
            {
                Items = items,
                TotalCount = totalCount,
                Page = query.Page,
                PageSize = pageSize
            };
        }
        
        public async Task ApprovePaymentAsync(int transactionId, int adminId, CancellationToken cancellationToken = default)
        {

            // =========================
            // Begin DB Transaction
            // =========================

            await using var dbTransaction =
                await _context.Database.BeginTransactionAsync(cancellationToken);

            // =========================
            // Get Root Order
            // =========================

            var rootOrder = await _context.Transactions
                .Include(t => t.PaymentProof)
                .FirstOrDefaultAsync(
                    t => t.Id == transactionId,
                    cancellationToken);

            // =========================
            // Validation: Exists
            // =========================

            if (rootOrder is null)
                throw new ConflictException("Order not found.");

            // =========================
            // Validation: Has payment proof
            // =========================

            if (rootOrder.PaymentProofId is null)
                throw new ConflictException("Payment proof not found.");

            // =========================
            // Validation: Correct state
            // =========================

            if (rootOrder.TransactionTypeId !=
                OrderTransactionTypeEnum.PendingPaymentVerification.ToId())
            {
                throw new BusinessRuleException("Payment request already processed.");
            }

            // =========================
            // Prevent duplicate escrow
            // =========================

            var escrowAlreadyExists = await _context.Transactions
                .AnyAsync(t =>
                    t.ParentTransactionId == rootOrder.Id &&
                    t.TransactionTypeId ==
                        FinancialTransactionTypeEnum.EscrowPayment.ToId(),
                    cancellationToken);

            if (escrowAlreadyExists)
                throw new BusinessRuleException("Escrow payment already exists.");

            // =========================
            // Update Root Order → Paid
            // =========================

            rootOrder.TransactionTypeId =
                OrderTransactionTypeEnum.Paid.ToId();

            rootOrder.UpdatedAt = DateTime.UtcNow;

            rootOrder.PaymentProof!.VerifiedAt = DateTime.UtcNow;
            rootOrder.PaymentProof!.IsVerified = true;
            rootOrder.PaymentProof!.VerifiedByAdminId = adminId;
            // =========================
            // Create Escrow Transaction
            // =========================

            var escrowTransaction = new Transaction
            {
                ParentTransactionId = rootOrder.Id,

                TransactionCode = Guid.NewGuid().ToString(),

                BuyerId = rootOrder.BuyerId,
                ProviderId = rootOrder.ProviderId,

                ServiceId = rootOrder.ServiceId,

                RawPrice = rootOrder.RawPrice,
                CashDiscount = rootOrder.CashDiscount,
                TotalPrice = rootOrder.TotalPrice + rootOrder.DeliveryFees,

                //CustomRequestText = rootOrder.CustomRequestText,
                //CustomRequestImage = rootOrder.CustomRequestImage,

                TransactionTypeId = FinancialTransactionTypeEnum.EscrowPayment.ToId(),

                PaymentProofId = rootOrder.PaymentProofId,

                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await _context.Transactions.AddAsync(
                escrowTransaction,
                cancellationToken
            );

            // =========================
            // Save Changes
            // =========================

            await _context.SaveChangesAsync(cancellationToken);

            // =========================
            // Commit
            // =========================

            await dbTransaction.CommitAsync(cancellationToken);


        }



        // Helper Method
        public static ServiceStatus ServiceStatusSwithcher(string status)
        {
            if (status == "Draft")
            {
                return ServiceStatus.Draft;
            }
            else if (status == "Active")
            {
                return ServiceStatus.Active;
            }
            else
            {
                return ServiceStatus.Archived;
            }
        }

    }
}
