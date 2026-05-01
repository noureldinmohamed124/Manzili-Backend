using Manzili.Application.Buyer.Queries.Services.GetPaginatedServices;
using Manzili.Application.Buyer.Queries.Services.GetServiceByName;
using Manzili.Application.Buyer.Queries.Services.GetServiceDetails;
using Manzili.Application.Seller.Queries.Services.GetAllSellerServices;
using Manzili.Application.Seller.Queries.Services.GetSellerServiceById;
using Manzili.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manzili.Application.Abstractions.Persistence
{
    public interface IServiceRepo : IGenericRepo<Service>
    {
        Task<HomeServicesDto> GetHomeServicesAsync(int take);
        Task<PaginatedServiceListDto> GetAllPaginatedForListingAsync(GetServicesQuery query);
        Task<ServiceDetailsDto?> GetServiceDetailsByIdAsync(int Id);
        Task<PagedResult<ServiceSearchDto>> SearchByNameAsync(SearchServicesQuery query);

        // For Seller
        Task<SellerServicesListDto> GetSellerServicesAsync(int sellerId, GetSellerServicesQuery q);
        Task<SellerServiceDetailsDto?> GetSellerServiceByIdAsync(int sellerId, int serviceId);
        Task<Service?> GetServiceDetailsForUpdateByIdAsync(int sellerId, int serviceId);
        Task<Service?> GetServiceByIdWithTransactionsAsync(int id);
    }
}
