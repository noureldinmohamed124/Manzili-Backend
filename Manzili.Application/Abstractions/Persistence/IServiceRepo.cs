using Manzili.Application.Queries.Services.GetPaginatedServices;
using Manzili.Application.Queries.Services.GetServiceByName;
using Manzili.Application.Queries.Services.GetServiceDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manzili.Application.Abstractions.Persistence
{
    public interface IServiceRepo
    {
        Task<HomeServicesDto> GetHomeServicesAsync(int take);
        Task<PaginatedServiceListDto> GetAllPaginatedForListingAsync(GetServicesQuery query);
        Task<ServiceDetailsDto?> GetServiceDetailsByIdAsync(int Id);
        Task<PagedResult<ServiceSearchDto>> SearchByNameAsync(SearchServicesQuery query);
    }
}
