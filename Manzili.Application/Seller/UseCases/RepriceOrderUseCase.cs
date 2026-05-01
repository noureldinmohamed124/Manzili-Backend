using Manzili.Application.Abstractions.Persistence;
using Manzili.Application.Abstractions.Security;
using Manzili.Application.Common.Enums;
using Manzili.Application.Common.Extensions;
using Manzili.Application.Exceptions;
using Manzili.Application.Seller.Commands.Orders.RePrice_Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manzili.Application.Seller.UseCases
{
    public class RepriceOrderUseCase
    {
        private readonly IOrderRepo _orderRepo;
        private readonly ICurrentUserService _currentUser;
        private readonly IUnitOfWork _unitOfWork;

        public RepriceOrderUseCase(IOrderRepo orderRepo, ICurrentUserService currentUser, IUnitOfWork unitOfWork)
        {
            _orderRepo = orderRepo;
            _currentUser = currentUser;
            _unitOfWork = unitOfWork;
        }

        public async Task ExecuteAsync(RepriceOrderCommand command)
        {
            int sellerId = _currentUser.UserId;

            var order = await _orderRepo.GetSellerOrderForUpdateAsync(sellerId, command.OrderId);

            if (order == null)
            {
                throw new NotFoundException("Order not found");
            }

            var currentStatus = (OrderTransactionTypeEnum)order.TransactionTypeId;

            if (!currentStatus.CanBeRepriced())
            {
                throw new ConflictException("This order cannot be repriced");
            }

            if (command.NewPrice <= 0)
            {
                throw new ValidationException("Invalid price");
            }

            if (string.IsNullOrWhiteSpace(command.Reason))
            {
                throw new ValidationException("Repricing reason is required");
            }

            // Save proposal only
            order.ProposedPrice = command.NewPrice;

            order.RePricingReason = command.Reason.Trim();

            order.TransactionTypeId = (int)OrderTransactionTypeEnum.RePriced;

            order.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
