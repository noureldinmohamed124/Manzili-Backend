using Manzili.Application.Abstractions.Persistence;
using Manzili.Application.Abstractions.Security;
using Manzili.Application.Exceptions;
using Manzili.Application.Seller.Queries.Orders.GetSellerOrderDetailsById;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manzili.Application.Seller.UseCases
{
    public class GetSellerOrderByIdUseCase
    {
        private readonly IOrderRepo _orderRepo;
        private readonly ICurrentUserService _currentUser;

        public GetSellerOrderByIdUseCase(IOrderRepo orderRepo, ICurrentUserService currentUser)
        {
            _orderRepo = orderRepo;
            _currentUser = currentUser;
        }

        public async Task<SellerOrderDetailsDto> ExecuteAsync(int orderId)
        {
            int sellerId = _currentUser.UserId;

            var order = await _orderRepo.GetSellerOrderByIdAsync(sellerId, orderId);

            if (order == null)
                throw new NotFoundException("Order not found");

            return order;
        }
    }
}
