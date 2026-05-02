using Manzili.Api.DTOs.Services;
using Manzili.Application.Buyer.Queries.Services.GetPaginatedServices;
using Manzili.Application.Buyer.Queries.Services.GetServiceByName;
using Manzili.Application.Buyer.Queries.Services.GetServiceDetails;
using Manzili.Application.Buyer.UseCases.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Manzili.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Buyer")]
    public class ServicesController : BaseApiController
    {
        private readonly GetHomeSectionUseCase _getHomeSectionUseCase;
        private readonly GetAllServicesUseCase _getAllServicesUseCase;
        private readonly GetServiceByIdUseCase _getServiceUseCase;
        private readonly SearchServicesUseCase _searchServicesUseCase;

        public ServicesController(GetHomeSectionUseCase getHomeSectionUseCase, GetAllServicesUseCase getAllServicesUseCase, GetServiceByIdUseCase getServiceUseCase, SearchServicesUseCase searchServicesUseCase)
        {
            _getHomeSectionUseCase = getHomeSectionUseCase;
            _getAllServicesUseCase = getAllServicesUseCase;
            _getServiceUseCase = getServiceUseCase;
            _searchServicesUseCase = searchServicesUseCase;
        }



        // Get All Services - The whole Home Section
        [HttpGet("home/{no}")]
        public async Task<IActionResult> GetHomeSection(int no)
        {
            var services = await _getHomeSectionUseCase.ExecuteAsync(no);
            return OkResponse(services);
        }

        // Get All Services
        [HttpGet]
        public async Task<IActionResult> GetAllServicesPaginated([FromQuery] GetServicesRequestDto dto)
        {
            var query = new GetServicesQuery
            {
                Page = dto.Page == 0 ? 1 : dto.Page,
                PageSize = dto.PageSize == 0 ? 10 : dto.PageSize,
                CategoryId = dto.CategoryId,
                Filter = dto.Filter,
                SortBy = dto.SortBy
            };

            var services = await _getAllServicesUseCase.ExecuteAsync(query);
            return OkResponse(services);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetServiceById(int id)
        {
            var query = new GetServiceDetailsQuery(ServiceId: id);

            var service = await _getServiceUseCase.ExecuteAsync(query);
            return OkResponse(service);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchServiceByName([FromQuery] SearchServicesByNameRequestDto dto)
        {
            var query = new SearchServicesQuery(
                Keyword: dto.Keyword,
                PageNumber: dto.PageNumber,
                PageSize: dto.PageSize
            );

            var services = await _searchServicesUseCase.ExecuteAsync(query);
            return OkResponse(services);
        }
    }
}
