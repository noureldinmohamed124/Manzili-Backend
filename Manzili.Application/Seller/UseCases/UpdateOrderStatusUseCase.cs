using Manzili.Application.Abstractions.Persistence;
using Manzili.Application.Abstractions.Security;
using Manzili.Application.Common.Enums;
using Manzili.Application.Common.Extensions;
using Manzili.Application.Exceptions;
using Manzili.Application.Seller.Commands.Orders.UpdateOrderStatus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manzili.Application.Seller.UseCases
{
    public class UpdateOrderStatusUseCase
    {
        private readonly IOrderRepo _orderRepo;
        private readonly ICurrentUserService _currentUser;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateOrderStatusUseCase(IOrderRepo orderRepo, ICurrentUserService currentUser, IUnitOfWork unitOfWork)
        {
            _orderRepo = orderRepo;
            _currentUser = currentUser;
            _unitOfWork = unitOfWork;
        }

        public async Task ExecuteAsync(UpdateOrderStatusCommand command)
        {
            int sellerId = _currentUser.UserId;

            var order = await _orderRepo.GetSellerOrderForUpdateAsync(sellerId, command.OrderId);

            if (order == null)
            {
                throw new NotFoundException("Order not found");
            }

            var currentStatus = (OrderTransactionTypeEnum)order.TransactionTypeId;

            // Validate workflow transition
            if (!currentStatus.CanTransitionTo(command.Status))
            {
                throw new ConflictException($"Cannot transition from " + $"{currentStatus} to " + $"{command.Status}");
            }

            // Update status
            order.TransactionTypeId = (int)command.Status;

            order.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
