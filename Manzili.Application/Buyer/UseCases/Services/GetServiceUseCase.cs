using Manzili.Application.Abstractions.Persistence;
using Manzili.Application.Buyer.Queries.Services.GetServiceDetails;
using Manzili.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manzili.Application.Buyer.UseCases.Services
{
    public class GetServiceUseCase
    {
        private readonly IServiceRepo _serviceRepo;

        public GetServiceUseCase(IServiceRepo serviceRepo)
        {
            _serviceRepo = serviceRepo;
        }

        public async Task<ServiceDetailsDto> ExecuteAsync(GetServiceDetailsQuery query)
        {
            var service = await _serviceRepo.GetServiceDetailsByIdAsync(query.ServiceId);

            if (service == null)
                throw new NotFoundException($"Service with Id {query.ServiceId} not found");

            return service;
        }
    }
}
