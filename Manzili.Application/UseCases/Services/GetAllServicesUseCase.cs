using Manzili.Application.Abstractions.Persistence;
using Manzili.Application.Queries.Services.GetPaginatedServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manzili.Application.UseCases.Services
{
    public class GetAllServicesUseCase
    {
        private readonly IServiceRepo _serviceRepo;

        public GetAllServicesUseCase(IServiceRepo serviceRepo)
        {
            _serviceRepo = serviceRepo;
        }

        public async Task<PaginatedServiceListDto> ExecuteAsync(GetServicesQuery query)
        {
            return await _serviceRepo.GetAllPaginatedForListingAsync(query);
        }
    }
}
