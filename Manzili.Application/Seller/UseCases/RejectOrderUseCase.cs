using Manzili.Application.Abstractions.Persistence;
using Manzili.Application.Abstractions.Security;
using Manzili.Application.Common.Enums;
using Manzili.Application.Common.Extensions;
using Manzili.Application.Exceptions;
using Manzili.Application.Seller.Commands.Orders.RejectOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manzili.Application.Seller.UseCases
{
    public class RejectOrderUseCase
    {
        private readonly IOrderRepo _orderRepo;
        private readonly ICurrentUserService _currentUser;
        private readonly IUnitOfWork _unitOfWork;

        public RejectOrderUseCase(IOrderRepo orderRepo, ICurrentUserService currentUser, IUnitOfWork unitOfWork)
        {
            _orderRepo = orderRepo;
            _currentUser = currentUser;
            _unitOfWork = unitOfWork;
        }

        public async Task ExecuteAsync(RejectOrderCommand command)
        {
            int sellerId = _currentUser.UserId;

            var order = await _orderRepo.GetSellerOrderForUpdateAsync(sellerId, command.OrderId);

            if (order == null)
            {
                throw new NotFoundException("Order not found");
            }

            var currentStatus = (OrderTransactionTypeEnum)order.TransactionTypeId;

            if (!currentStatus.CanBeRejected())
            {
                throw new ConflictException("This order cannot be rejected");
            }

            if (string.IsNullOrWhiteSpace(command.Reason))
            {
                throw new ValidationException("Rejection reason is required");
            }

            order.TransactionTypeId = (int)OrderTransactionTypeEnum.Rejected;
            order.RejectionReason = command.Reason.Trim();

            order.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
