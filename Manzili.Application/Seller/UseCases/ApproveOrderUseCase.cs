using Manzili.Application.Abstractions.Persistence;
using Manzili.Application.Abstractions.Security;
using Manzili.Application.Common.Enums;
using Manzili.Application.Exceptions;
using Manzili.Application.Seller.Commands.Orders.ApproveOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manzili.Application.Seller.UseCases
{
    public class ApproveOrderUseCase
    {
        private readonly IOrderRepo _orderRepo;
        private readonly ICurrentUserService _currentUser;
        private readonly IUnitOfWork _unitOfWork;

        public ApproveOrderUseCase(IOrderRepo orderRepo, ICurrentUserService currentUser, IUnitOfWork unitOfWork)
        {
            _orderRepo = orderRepo;
            _currentUser = currentUser;
            _unitOfWork = unitOfWork;
        }

        public async Task ExecuteAsync(
        ApproveOrderCommand command)
        {
            int sellerId = _currentUser.UserId;

            var order = await _orderRepo.GetSellerOrderForUpdateAsync(sellerId, command.OrderId);

            if (order == null)
            {
                throw new NotFoundException("Order not found");
            }

            var currentStatus = (OrderTransactionTypeEnum)order.TransactionTypeId;

            // ONLY Request can be approved
            if (currentStatus != OrderTransactionTypeEnum.Request)
            {
                throw new ConflictException("Only requested orders can be approved");
            }

            // Change status
            order.TransactionTypeId = (int)OrderTransactionTypeEnum.Accepted;

            order.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
