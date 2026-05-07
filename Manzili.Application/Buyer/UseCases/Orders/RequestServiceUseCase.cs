using Manzili.Application.Abstractions.Persistence;
using Manzili.Application.Abstractions.Security;
using Manzili.Application.Buyer.Commands.Orders;
using Manzili.Application.Common.Extensions;
using Manzili.Application.Common.Interfaces;
using Manzili.Application.Exceptions;
using Manzili.Domain.Entities;
using Manzili.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manzili.Application.Buyer.UseCases.Orders
{
    public class RequestServiceUseCase
    {
        private readonly IServiceRepo _serviceRepo;
        private readonly IOrderRepo _orderRepo;
        private readonly IUserRepo _userRepo;
        private readonly IServiceOptionRepo _optionRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUserService;
        private readonly ITransactionCodeGenerator _transactionCodeGenerator;

        public RequestServiceUseCase(IServiceRepo serviceRepo, IOrderRepo orderRepo, IUserRepo userRepo, IServiceOptionRepo optionRepo, IUnitOfWork unitOfWork, ICurrentUserService currentUserService, ITransactionCodeGenerator transactionCodeGenerator)
        {
            _serviceRepo = serviceRepo;
            _orderRepo = orderRepo;
            _userRepo = userRepo;
            _optionRepo = optionRepo;
            _unitOfWork = unitOfWork;
            _currentUserService = currentUserService;
            _transactionCodeGenerator = transactionCodeGenerator;
        }

        public async Task<int> ExecuteAsync(RequestServiceCommand command)
        {
            int customerId = _currentUserService.UserId;

            await ValidateRequestAsync(command.ServiceId, customerId);

            var service = await _serviceRepo.GetByIdAsync(command.ServiceId);
            

            decimal subtotal = 0;
            decimal optionsPrice = 0;
            subtotal = service!.BasePrice;

            var transaction = new Transaction
            {
                CustomRequestText = command.CustomizationText ?? "لا يوجد اي ملاحظات",
                CustomRequestImage = command.CustomRequestImage,
                BuyerId = customerId,
                ProviderId = service.ProviderId,
                ServiceId = service.Id,
                TransactionTypeId = TransactionStatus.Request.ToId()
            };

            foreach(var optionGroup in command.OptionGroups)
            {
                foreach (var optionItem in optionGroup.Options)
                {
                    var serviceOption = await _optionRepo.GetByIdAsync(optionItem.OptionId);

                    if (serviceOption == null)
                        throw new NotFoundException("This Option doesn't exist");

                    transaction.TransactionOptions.Add(new TransactionOption
                    {
                        OptionName = serviceOption.ServiceOptionName,
                        Price = serviceOption.PriceAdjustment,
                        Quantity = optionItem.Quantity,
                        ServiceOptionGroupId = serviceOption.OptionGroupId,
                        ServiceOptionId = serviceOption.Id
                    });

                    optionsPrice += (serviceOption.PriceAdjustment ?? 0) * optionItem.Quantity;
                }
            }

            decimal cashDiscount = 0;
            decimal deliveryFees = 40m;

            transaction.RawPrice = subtotal;
            transaction.CashDiscount = cashDiscount;
            transaction.DeliveryFees = deliveryFees;
            transaction.TotalPrice = subtotal + optionsPrice + deliveryFees - cashDiscount;


            await _orderRepo.AddAsync(transaction);
            await _unitOfWork.SaveChangesAsync();

            transaction.TransactionCode = _transactionCodeGenerator.GenerateOrderCode(transaction.Id);

            await _unitOfWork.SaveChangesAsync();
            return transaction.Id;
        }


        private async Task ValidateRequestAsync(int serviceId, int customerId)
        {
            var service = await _serviceRepo.GetByIdAsync(serviceId);

            if (service == null)
                throw new NotFoundException("Service not found");

            var provider = await _userRepo.GetByIdAsync(service.ProviderId);

            if (provider == null)
                throw new ConflictException("Provider of this Service not available right now");

            if (provider.IsBlocked)
                throw new ConflictException("This Provider/Service not avalible right now");

            if (service.ProviderId == customerId)
                throw new ConflictException("You can not request your own service");
        }
    }
}


/*CancellationPolicy
 
public class CancellationPolicy : ICancellationPolicy
{
    public CancellationResult Calculate(Transaction order)
    {
        bool isPaid = order.Children.Any(t => t.TransactionTypeId == TransactionTypes.EscrowPayment);
        bool isCompleted = order.Children.Any(t => t.TransactionTypeId == TransactionTypes.SellerPayout);

        if (!isPaid)
        {
            return new CancellationResult
            {
                RefundAmount = 0,
                PlatformFee = 0,
                SellerCompensation = 0
            };
        }

        if (isPaid && !isCompleted)
        {
            return new CancellationResult
            {
                RefundAmount = order.TotalPrice * 0.9m,
                PlatformFee = order.TotalPrice * 0.1m,
                SellerCompensation = 0
            };
        }

        throw new BusinessRuleException("Cannot cancel completed order");
    }
}
*/