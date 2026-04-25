using Manzili.Application.Abstractions.Persistence;
using Manzili.Application.Abstractions.Security;
using Manzili.Application.Commands.Orders.SubmitPayment;
using Manzili.Application.Common.Enums;
using Manzili.Application.Exceptions;
using Manzili.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manzili.Application.UseCases.Orders
{
    public class SubmitPaymentUseCase
    {
        private readonly IOrderRepo _orderRepo;
        private readonly IPaymentProofRepo _paymentProofRepo;
        private readonly ICurrentUserService _currentUser;

        public SubmitPaymentUseCase(IOrderRepo orderRepo, IPaymentProofRepo paymentProofRepo, ICurrentUserService currentUser)
        {
            _orderRepo = orderRepo;
            _paymentProofRepo = paymentProofRepo;
            _currentUser = currentUser;
        }

        public async Task<PaymentSuccessDto> ExecuteAsync(SubmitPaymentCommand command)
        {
            int buyerId = _currentUser.UserId;

            var orders = await _orderRepo.GetOrdersForPaymentAsync(buyerId, command.OrderIds);


            // Validate Orders
            if (!orders.Any())
                throw new NotFoundException("Orders not found");

            if (orders.Count != command.OrderIds.Count)
                throw new ConflictException("Some Orders are invalid");

            if (orders.Any(o => o.TransactionTypeId != (int)OrderTransactionTypeEnum.Accepted))
                throw new BusinessRuleException("Only accepted orders can be paid");


            var paymentProof = new PaymentProof
            {
                ScreenshotUrl = command.PaymentScreenshot,
                Notes = command.Notes
            };


            foreach (var order in orders)
            {
                order.TransactionTypeId = (int)OrderTransactionTypeEnum.PendingPaymentVerification;
                order.PaymentProof = paymentProof;
            }

            await _paymentProofRepo.AddAsync(paymentProof);

            decimal total = orders.Sum(o => o.TotalPrice);

            var paymentSuccessDto = new PaymentSuccessDto
            {
                OrderNo = string.Join(",", orders.Select(o => o.Id)),
                PaymentDate = DateTime.UtcNow,
                Total = total,
            };
            
            return paymentSuccessDto;
        }
    }
}
