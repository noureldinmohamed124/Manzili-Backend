using Manzili.Application.Abstractions.Persistence;
using Manzili.Application.Admin.Orders.Queries.GetAdminOrders;
using Manzili.Application.Admin.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manzili.Application.Admin.Orders.UseCases
{
    public class GetAdminOrdersUseCase
    {
        private readonly IAdminRepo _adminRepo;

        public GetAdminOrdersUseCase(IAdminRepo adminRepo)
        {
            _adminRepo = adminRepo;
        }

        public async Task<PagedResult<AdminOrderDto>> ExecuteAsync(GetAdminOrdersQuery query, CancellationToken cancellationToken = default)
        {
            return await _adminRepo.GetOrdersAsync(query, cancellationToken);
        }
    }
}
