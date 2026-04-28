using Manzili.Application.Abstractions.Persistence;
using Manzili.Application.Abstractions.Security;
using Manzili.Application.Seller.Queries.Services.GetAllSellerServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manzili.Application.Seller.UseCases
{
    public class GetSellerServicesUseCase
    {
        private readonly IServiceRepo _serviceRepo;
        private readonly ICurrentUserService _currentUser;
        public GetSellerServicesUseCase(IServiceRepo serviceRepo, ICurrentUserService currentUser)
        {
            _serviceRepo = serviceRepo;
            _currentUser = currentUser;
        }

        public async Task<SellerServicesListDto> ExecuteAsync(GetSellerServicesQuery query)
        {
            int sellerId = _currentUser.UserId;
            return await _serviceRepo.GetSellerServicesAsync(sellerId, query);
        }
    }
}
