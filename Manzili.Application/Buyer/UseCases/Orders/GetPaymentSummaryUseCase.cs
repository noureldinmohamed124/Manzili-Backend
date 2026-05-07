using Manzili.Application.Abstractions.Persistence;
using Manzili.Application.Abstractions.Security;
using Manzili.Application.Buyer.Queries.Orders.GetPaymentSummary;
using Manzili.Application.Common.Enums;
using Manzili.Application.Common.Extensions;
using Manzili.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manzili.Application.Buyer.UseCases.Orders
{
    public class GetPaymentSummaryUseCase
    {
        private readonly IOrderRepo _orderRepo;
        private readonly ICurrentUserService _currentUser;
        private readonly IAddressRepo _addressRepo;

        public GetPaymentSummaryUseCase(IOrderRepo orderRepo, ICurrentUserService currentUser, IAddressRepo addressRepo)
        {
            _orderRepo = orderRepo;
            _currentUser = currentUser;
            _addressRepo = addressRepo;
        }


        public async Task<PaymentSummaryDto> ExecuteAsync(GetPaymentSummaryQuery query)
        {
            var buyerId = _currentUser.UserId;

            var orders = await _orderRepo.GetOrdersForPaymentSummaryAsync(buyerId, query.OrderIds);

            if (!orders.Any())
                throw new NotFoundException("Orders Not Found");

            if (orders.Any(o => o.TransactionTypeId != OrderTransactionTypeEnum.Accepted.ToId()))
                throw new BusinessRuleException("Only Accepted Orders can be Paid");

            decimal subTotal = orders.Sum(o => o.RawPrice);
            Console.WriteLine($"Subtotal of all Orders (Raw price of all) : {subTotal}");


            decimal deliveryFees = 40m;
            
            var address = await _addressRepo.GetByBuyerIdAsync(buyerId);

            if (address == null)
                throw new NotFoundException("You don't have delivery Address, Should Enter 1 at First");

            var pServices = orders.Select(o => new PaymentSummaryServiceItemDto
            {
                OrderId = o.Id,
                Image = o.Service!.ServiceImages.FirstOrDefault()!.ImageUrl,
                Title = o.Service.Title,
                Quantity = 1,
                Price = o.RawPrice,

                Options = o.TransactionOptions.Select(op => new PaymentSummaryServiceOptionDto
                {
                    Name = op.OptionName,
                    Quantity = op.Quantity,
                    Price = op.Price ?? 0
                }).ToList(),

            }).ToList();

            decimal optionsPrice = 0m;
            int c = 1;
            foreach(var service in pServices)
            {
                optionsPrice += service.Options.Sum(o => o.Price * o.Quantity);
                Console.WriteLine($"Service '{c}' Options Price: {optionsPrice}");
                c++;
            }
            subTotal += optionsPrice;
            Console.WriteLine($"SubTotal after Options Price : {subTotal}");

            var pAddress = new PaymentSummaryAddressDto
            {
                AddressPreview = address.FullAddress,
                Phone = address.User != null ? address.User.PhoneNumber ?? "No Specified Phone Number" : "No Specified Phone Number"
            };

            var pBreakDown = new PaymentBreakdownDto
            {
                Subtotal = subTotal,
                DeliveryFees = deliveryFees,
                Total = subTotal + deliveryFees,
            };

            var paymentSummaryDto = new PaymentSummaryDto
            {
                Services = pServices,
                Address = pAddress,
                PriceBreakdown = pBreakDown
            };


            return paymentSummaryDto;
        }
    }
}
