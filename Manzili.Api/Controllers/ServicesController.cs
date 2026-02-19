using Manzili.Api.DTOs.Services;
using Manzili.Application.Queries.Services.GetPaginatedServices;
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

        public ServicesController(GetHomeSectionUseCase getHomeSectionUseCase, GetAllServicesUseCase getAllServicesUseCase, GetServiceUseCase getServiceUseCase)
        {
            _getHomeSectionUseCase = getHomeSectionUseCase;
            _getAllServicesUseCase = getAllServicesUseCase;
            _getServiceUseCase = getServiceUseCase;
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
                IsRecommended = dto.IsRecommended,
                MostPurchased = dto.MostPurchased,
                TopDiscounts = dto.TopDiscounts
            };

            var services = await _getAllServicesUseCase.ExecuteAsync(query);
            return OkResponse(services);
        }

        [HttpGet("{id:int}")]
        [AllowAnonymous]
        [Authorize(Roles = "Provider,Buyer")]
        public async Task<IActionResult> GetServiceByIdAsync(int id)
        {
            var service = await _getServiceUseCase.ExecuteAsync(id);
            return OkResponse(service);
        }
    }
}
