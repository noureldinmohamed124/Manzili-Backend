using Manzili.Application.Abstractions.Persistence;
using Manzili.Application.Abstractions.Security;
using Manzili.Application.Exceptions;
using Manzili.Application.Seller.Queries.Services.GetSellerServiceById;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manzili.Application.Seller.UseCases
{
    public class GetSellerServiceByIdUseCase
    {
        private readonly IServiceRepo _serviceRepo;
        private readonly ICurrentUserService _currentUser;

        public GetSellerServiceByIdUseCase(IServiceRepo serviceRepo, ICurrentUserService currentUser)
        {
            _serviceRepo = serviceRepo;
            _currentUser = currentUser;
        }

        public async Task<SellerServiceDetailsDto> ExecuteAsync(GetSellerServiceByIdQuery query)
        {
            int sellerId = _currentUser.UserId;
            var service = await _serviceRepo.GetSellerServiceByIdAsync(sellerId, query.ServiceId);

            if (service == null)
                throw new NotFoundException("Service not found");

            return service;
        }
    }
}
