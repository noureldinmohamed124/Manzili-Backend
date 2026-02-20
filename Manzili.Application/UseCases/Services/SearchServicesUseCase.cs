using Manzili.Application.Abstractions.Persistence;
using Manzili.Application.Queries.Services.GetServiceByName;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manzili.Application.UseCases.Services
{
    public class SearchServicesUseCase
    {
        private readonly IServiceRepo _serviceRepo;

        public SearchServicesUseCase(IServiceRepo serviceRepo)
        {
            _serviceRepo = serviceRepo;
        }

        public async Task<PagedResult<ServiceSearchDto>> ExecuteAsync(SearchServicesQuery query)
        {
            return await _serviceRepo.SearchByNameAsync(query);
        }
    }
}
