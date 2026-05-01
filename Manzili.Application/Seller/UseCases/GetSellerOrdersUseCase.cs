using Manzili.Application.Abstractions.Persistence;
using Manzili.Application.Abstractions.Security;
using Manzili.Application.Seller.Queries.Orders.GetAllSellerOrders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manzili.Application.Seller.UseCases
{
    public class GetSellerOrdersUseCase
    {
        private readonly IOrderRepo _orderRepo;
        private readonly ICurrentUserService _currentUser;

        public GetSellerOrdersUseCase(IOrderRepo orderRepo, ICurrentUserService currentUser)
        {
            _orderRepo = orderRepo;
            _currentUser = currentUser;
        }

        public async Task<SellerOrdersListDto> ExecuteAsync(GetSellerOrdersQuery query)
        {
            int sellerId = _currentUser.UserId;
            return await _orderRepo.GetSellerOrdersAsync(sellerId, query);
        }
    }
}
