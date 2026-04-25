using Manzili.Application.Abstractions.Persistence;
using Manzili.Application.Abstractions.Security;
using Manzili.Application.Queries.Orders.GetAllOrders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manzili.Application.UseCases.Orders
{
    public class GetAllOrdersUseCase
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IOrderRepo _orderRepo;

        public GetAllOrdersUseCase(ICurrentUserService currentUserService, IOrderRepo orderRepo)
        {
            _currentUserService = currentUserService;
            _orderRepo = orderRepo;
        }


        public async Task<OrdersListDto> ExecuteAsync(GetOrdersQuery query)
        {
            var userId = _currentUserService.UserId;

            return await _orderRepo.GetAllOrdersAsync(userId, query.Status);

        }
    }
}
