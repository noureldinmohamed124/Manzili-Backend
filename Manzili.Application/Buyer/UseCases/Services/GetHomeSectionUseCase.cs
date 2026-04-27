using Manzili.Application.Abstractions.Persistence;
using Manzili.Application.Buyer.Queries.Services.GetPaginatedServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manzili.Application.Buyer.UseCases.Services
{
    public class GetHomeSectionUseCase
    {
        private readonly IServiceRepo _serviceRepo;

        public GetHomeSectionUseCase(IServiceRepo serviceRepo)
        {
            _serviceRepo = serviceRepo;
        }

        public async Task<HomeServicesDto> ExecuteAsync(int no)
        {
            return await _serviceRepo.GetHomeServicesAsync(no);
        }
    }
}
