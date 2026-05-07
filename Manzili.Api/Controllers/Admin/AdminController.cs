using Manzili.Api.Common;
using Manzili.Api.DTOs.Admin;
using Manzili.Application.Admin.Dashboard;
using Manzili.Application.Admin.Financials.Queries;
using Manzili.Application.Admin.Financials.UseCases;
using Manzili.Application.Admin.Orders.Queries.GetAdminOrders;
using Manzili.Application.Admin.Orders.UseCases;
using Manzili.Application.Admin.Payments.Commands;
using Manzili.Application.Admin.Payments.Queries.GetAllPaymentRequests;
using Manzili.Application.Admin.Payments.UseCases;
using Manzili.Application.Admin.Services.Queries.GetAdminServices;
using Manzili.Application.Admin.Services.UseCases;
using Manzili.Application.Admin.Users.Commands.BlockUser;
using Manzili.Application.Admin.Users.Commands.UnBlockUser;
using Manzili.Application.Admin.Users.Queries;
using Manzili.Application.Admin.Users.Queries.GetAdminAllUsers;
using Manzili.Application.Admin.Users.Queries.GetAdminUserById;
using Manzili.Application.Admin.Users.UseCases;
using Manzili.Application.Seller.Queries.Services.GetDashboardStats;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Manzili.Api.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminController : BaseApiController
    {
        private readonly GetAdminDashboardStatsUseCase _getAdminDashboardStatsUseCase;
        private readonly GetAdminAllUsersUseCase _getAdminAllUsersUseCase;
        private readonly GetAdminUserDetailsUseCase _getAdminUserDetailsUseCase;
        private readonly BlockUserUseCase _blockUserUseCase;
        private readonly UnblockUserUseCase _unblockUserUseCase;
        private readonly GetAdminServicesUseCase _getAdminServicesUseCase;
        private readonly GetAdminOrdersUseCase _getAdminOrdersUseCase;
        private readonly GetAdminFinancialsUseCase _getAdminFinancialsUseCase;
        private readonly GetPaymentRequestsUseCase _getPaymentRequestsUseCase;
        private readonly ApprovePaymentUseCase _approvePaymentUseCase;
        private readonly RejectPaymentProofUseCase _rejectPaymentProofUseCase;
        private readonly GetPaymentProofDetailsUseCase _getPaymentProofDetailsUseCase;

        public AdminController(GetAdminDashboardStatsUseCase getAdminDashboardStatsUseCase, GetAdminAllUsersUseCase getAdminAllUsersUseCase, GetAdminUserDetailsUseCase getAdminUserDetailsUseCase, BlockUserUseCase blockUserUseCase, UnblockUserUseCase unblockUserUseCase, GetAdminServicesUseCase getAdminServicesUseCase, GetAdminOrdersUseCase getAdminOrdersUseCase, GetAdminFinancialsUseCase getAdminFinancialsUseCase, GetPaymentRequestsUseCase getPaymentRequestsUseCase, ApprovePaymentUseCase approvePaymentUseCase, RejectPaymentProofUseCase rejectPaymentProofUseCase, GetPaymentProofDetailsUseCase getPaymentProofDetailsUseCase)
        {
            _getAdminDashboardStatsUseCase = getAdminDashboardStatsUseCase;
            _getAdminAllUsersUseCase = getAdminAllUsersUseCase;
            _getAdminUserDetailsUseCase = getAdminUserDetailsUseCase;
            _blockUserUseCase = blockUserUseCase;
            _unblockUserUseCase = unblockUserUseCase;
            _getAdminServicesUseCase = getAdminServicesUseCase;
            _getAdminOrdersUseCase = getAdminOrdersUseCase;
            _getAdminFinancialsUseCase = getAdminFinancialsUseCase;
            _getPaymentRequestsUseCase = getPaymentRequestsUseCase;
            _approvePaymentUseCase = approvePaymentUseCase;
            _rejectPaymentProofUseCase = rejectPaymentProofUseCase;
            _getPaymentProofDetailsUseCase = getPaymentProofDetailsUseCase;
        }

        [HttpGet("dashboard")]
        public async Task<IActionResult> GetDashboardStats()
        {
            var query = new GetDashboardStatsQuery();

            var result = await _getAdminDashboardStatsUseCase.ExecuteAsync(query);

            return Ok(result);
        }


        [HttpGet("users")]
        public async Task<IActionResult> GetUsers([FromQuery] GetAdminAllUsersRequestDto dto, CancellationToken cancellationToken)
        {
            var query = new GetAdminAllUsersQuery
            {
                Page = dto.Page,
                PageSize = dto.PageSize,
                IsBlocked = dto.IsBlocked,
                Role = dto.Role,
                Search = dto.Search
            };

            var result = await _getAdminAllUsersUseCase.ExecuteAsync(query, cancellationToken);

            return OkResponse(result);
        }



        [HttpGet("services")]
        public async Task<IActionResult> GetServices([FromQuery] GetAdminServicesRequestDto dto, CancellationToken cancellationToken)
        {
            var query = new GetAdminServicesQuery
            {
                Page = dto.Page,
                PageSize = dto.PageSize,
                ProviderId = dto.ProviderId,
                Search = dto.Search,
                Status = dto.Status
            };

            var result = await _getAdminServicesUseCase.ExecuteAsync(query, cancellationToken);

            return OkResponse(result);
        }


        [HttpGet("orders")]
        public async Task<IActionResult> GetOrders([FromQuery] GetAdminOrdersRequestDto dto, CancellationToken cancellationToken)
        {
            var query = new GetAdminOrdersQuery
            {
                BuyerId = dto.BuyerId,
                Page = dto.Page,
                PageSize = dto.PageSize,
                ProviderId = dto.ProviderId,
                Status = dto.Status
            };

            var result = await _getAdminOrdersUseCase.ExecuteAsync(query, cancellationToken);

            return OkResponse(result);
        }


        // Users Management

        [HttpGet("users/{id}")]
        public async Task<IActionResult> GetUserDetails(int id, CancellationToken cancellationToken)
        {
            var query = new GetAdminUserDetailsQuery
            {
                UserId = id,
            };

            var result = await _getAdminUserDetailsUseCase.ExecuteAsync(query, cancellationToken);

            return OkResponse(result);
        }



        [HttpPatch("users/{id}/block")]
        public async Task<IActionResult> BlockUser(int id, [FromBody] BlockUserRequestDto dto, CancellationToken cancellationToken)
        {
            var query = new BlockUserCommand
            {
                UserId = id,
                Reason = dto.Reason,
                BlockedUntil = dto.BlockedUntil
            };
            await _blockUserUseCase.ExecuteAsync(query, cancellationToken);

            return OkResponse(Messages.Admin.Blocked);
        }


        [HttpPatch("users/{id}/unblock")]
        public async Task<IActionResult> UnblockUser(int id, CancellationToken cancellationToken)
        {
            var command = new UnblockUserCommand
            {
                UserId = id
            };

            await _unblockUserUseCase.ExecuteAsync(command, cancellationToken);
            return OkResponse(Messages.Admin.Unblocked);
        }


        // Financials / Payments


        [HttpGet("financials")]
        public async Task<IActionResult> GetFinancials([FromQuery] GetAdminFinancialsRequestDto dto, CancellationToken cancellationToken)
        {
            var query = new GetAdminFinancialsQuery
            {
                BuyerId = dto.BuyerId,
                From = dto.From,
                Page = dto.Page,
                PageSize = dto.PageSize,
                ProviderId = dto.ProviderId
            };

            var result = await _getAdminFinancialsUseCase.ExecuteAsync(query, cancellationToken);

            return OkResponse(result);
        }


        [HttpGet("payments")]
        public async Task<IActionResult> GetPaymentRequests([FromQuery] GetPaymentProofsRequestsDto dto, CancellationToken cancellationToken)
        {
            var query = new GetPaymentRequestsQuery
            {
                BuyerId = dto.BuyerId,
                From = dto.From,
                Page = dto.Page,
                PageSize = dto.PageSize,
                ProviderId = dto.ProviderId,
                To = dto.To,
                IsVerified = dto.IsVerified
            };

            var result = await _getPaymentRequestsUseCase.ExecuteAsync(query, cancellationToken);

            return OkResponse(result);
        }


        [HttpPost("payment/approve")]
        public async Task<IActionResult> ApprovePayment([FromBody] ApprovePaymentProofRequestDto dto, CancellationToken cancellationToken)
        {
            await _approvePaymentUseCase.ExecuteAsync(dto.TransactionId, cancellationToken);

            return OkResponse(Messages.Admin.ApprovePaymentProof);
        }


        [HttpPost("payment/reject")]
        public async Task<IActionResult> RejectPayment([FromBody] RejectPaymentProofRequestDto dto, CancellationToken cancellationToken)
        {
            var command = new RejectPaymentProofCommand
            {
                RejectionReason = dto.RejectionReason,
                TransactionId = dto.TransactionId,
            };

            await _rejectPaymentProofUseCase.ExecuteAsync(command, cancellationToken);

            return OkResponse(Messages.Admin.RejectPaymentProof);
        }


        [HttpGet("payment/{id}")]
        public async Task<IActionResult> GetPaymentRequestDetails(int id, CancellationToken cancellationToken)
        {
            var result = await _getPaymentProofDetailsUseCase.ExecuteAsync(id, cancellationToken);
            return OkResponse(result);
        }


    }
}
