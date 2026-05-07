using Manzili.Application.Admin.Dashboard.Queries.GetAdminDashboardStats;
using Manzili.Application.Admin.Financials.Queries;
using Manzili.Application.Admin.Orders.Queries.GetAdminOrders;
using Manzili.Application.Admin.Payments.Commands;
using Manzili.Application.Admin.Payments.Queries.GetAllPaymentRequests;
using Manzili.Application.Admin.Payments.Queries.GetPaymentProofById;
using Manzili.Application.Admin.Services.Queries.GetAdminServices;
using Manzili.Application.Admin.Users;
using Manzili.Application.Admin.Users.Queries.GetAdminAllUsers;
using Manzili.Application.Admin.Users.Queries.GetAdminUserById;
using Manzili.Application.Seller.Queries.Services.GetDashboardStats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manzili.Application.Abstractions.Persistence
{
    public interface IAdminRepo
    {
        Task<AdminDashboardStatsDto> GetDashboardStatsAsync(CancellationToken cancellationToken = default);

        Task<PagedResult<AdminUserDto>> GetAdminAllUsersAsync(GetAdminAllUsersQuery query, CancellationToken cancellationToken = default);

        Task<AdminUserDetailsDto?> GetUserDetailsByIdAsync(int userId, CancellationToken cancellationToken = default);

        Task<PagedResult<AdminServiceDto>> GetServicesAsync(GetAdminServicesQuery query, CancellationToken cancellationToken = default);

        Task<PagedResult<AdminOrderDto>> GetOrdersAsync(GetAdminOrdersQuery query, CancellationToken cancellationToken = default);

        Task<AdminFinancialsResult> GetFinancialsAsync(GetAdminFinancialsQuery query, CancellationToken cancellationToken = default);


        Task<PagedResult<PaymentProofsRequestDto>> GetPaymentRequestsAsync(GetPaymentRequestsQuery query, CancellationToken cancellationToken = default);


        // Approve Payment Proof Request
        Task ApprovePaymentAsync(int transactionId, int adminId, CancellationToken cancellationToken = default);


        // Reject Payment Proof Request
        Task RejectPaymentAsync(RejectPaymentProofCommand command, CancellationToken cancellationToken = default);


        // Get Payment Proof Details By Id
        Task<PaymentProofDetailsDto> GetPaymentProofDetailsAsync(int transactionId, CancellationToken cancellationToken = default);

    }
}
