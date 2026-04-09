using Manzili.Application.Abstractions.Persistence;
using Manzili.Application.Abstractions.Security;
using Manzili.Application.Queries.Orders.GetAcceptedOrders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manzili.Application.UseCases.Orders
{
    public class GetAcceptedOrdersUseCase
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IOrderRepo _orderRepo;

        public GetAcceptedOrdersUseCase(ICurrentUserService currentUserService, IOrderRepo orderRepo)
        {
            _currentUserService = currentUserService;
            _orderRepo = orderRepo;
        }


        public async Task<AcceptedOrdersListDto> ExecuteAsync()
        {
            var userId = _currentUserService.UserId;

            

        }
    }
}
