using Manzili.Api.DTOs.Services;
using Manzili.Application.Queries.Services.GetPaginatedServices;
using Manzili.Application.Queries.Services.GetServiceByName;
using Manzili.Application.Queries.Services.GetServiceDetails;
using Manzili.Application.UseCases.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Manzili.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : BaseApiController
    {
        private readonly GetHomeSectionUseCase _getHomeSectionUseCase;
        private readonly GetAllServicesUseCase _getAllServicesUseCase;
        private readonly GetServiceUseCase _getServiceUseCase;
        private readonly SearchServicesUseCase _searchServicesUseCase;

        public ServicesController(GetHomeSectionUseCase getHomeSectionUseCase, GetAllServicesUseCase getAllServicesUseCase, GetServiceUseCase getServiceUseCase, SearchServicesUseCase searchServicesUseCase)
        {
            _getHomeSectionUseCase = getHomeSectionUseCase;
            _getAllServicesUseCase = getAllServicesUseCase;
            _getServiceUseCase = getServiceUseCase;
            _searchServicesUseCase = searchServicesUseCase;
        }



        // Get All Services - The whole Home Section
        [HttpGet("home/{no}")]
        [AllowAnonymous]
        [Authorize(Roles = "Provider,Buyer")]
        public async Task<IActionResult> GetHomeSection(int no)
        {
            var services = await _getHomeSectionUseCase.ExecuteAsync(no);
            return OkResponse(services);
        }

        // Get All Services
        [HttpGet]
        [AllowAnonymous]
        [Authorize(Roles = "Provider,Buyer")]
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
        [AllowAnonymous]
        [Authorize(Roles = "Provider,Buyer")]
        public async Task<IActionResult> GetServiceById(int id)
        {
            var query = new GetServiceDetailsQuery(ServiceId: id);

            var service = await _getServiceUseCase.ExecuteAsync(query);
            return OkResponse(service);
        }

        [HttpGet("search")]
        [AllowAnonymous]
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
