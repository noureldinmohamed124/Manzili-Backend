using Manzili.Api.Common;
using Manzili.Api.DTOs.Seller;
using Manzili.Application.Buyer.Queries.Services.GetServiceDetails;
using Manzili.Application.Seller.Commands.Services.CreateService;
using Manzili.Application.Seller.Queries.Services.GetAllSellerServices;
using Manzili.Application.Seller.Queries.Services.GetSellerServiceById;
using Manzili.Application.Seller.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Manzili.Api.Controllers.Seller
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Provider")]
    public class SellerController : BaseApiController
    {
        private readonly GetDashboardStatsUseCase _getDashboardStatsUseCase;
        private readonly GetSellerServicesUseCase _getSellerServicesUseCase;
        private readonly GetSellerServiceByIdUseCase _getSellerServiceByIdUseCase;
        private readonly CreateServiceUseCase _createServiceUseCase;

        public SellerController(GetDashboardStatsUseCase getDashboardStatsUseCase, GetSellerServicesUseCase getSellerServicesUseCase, GetSellerServiceByIdUseCase getSellerServiceByIdUseCase, CreateServiceUseCase createServiceUseCase)
        {
            _getDashboardStatsUseCase = getDashboardStatsUseCase;
            _getSellerServicesUseCase = getSellerServicesUseCase;
            _getSellerServiceByIdUseCase = getSellerServiceByIdUseCase;
            _createServiceUseCase = createServiceUseCase;
        }

        [HttpGet("dashboard")]
        public async Task<IActionResult> GetDashboardStats()
        {
            var result = await _getDashboardStatsUseCase.ExecuteAsync();

            return OkResponse(result);
        }


        [HttpGet("services")]
        public async Task<IActionResult> GetAllServices(GetSellerServicesRequestDto dto)
        {
            var query = new GetSellerServicesQuery
            {
                Status = dto.status,
                Page = dto.Page,
                PageSize = dto.PageSize
            };
            var result = await _getSellerServicesUseCase.ExecuteAsync(query);
            return OkResponse(result);
        }

        [HttpGet("services/{id}")]
        public async Task<IActionResult> GetSellerServiceById(int id)
        {
            var query = new GetSellerServiceByIdQuery(id);
            var result = await _getSellerServiceByIdUseCase.ExecuteAsync(query);

            return OkResponse(result);
        }


        [HttpPost("services")]
        public async Task<IActionResult> CreateService(CreateServiceDto dto)
        {
            var command = new CreateServiceCommand(
                Title: dto.Title,
                Description: dto.Description,
                CategoryId: dto.CategoryId,
                BasePrice: dto.BasePrice,
                Images: dto.Images,
                OptionGroups: dto.OptionGroups.Select(g => new CreateOptionGroupDto {
                    Name = g.Name,
                    IsRequired = g.IsRequired,
                    Options = g.Options.Select(o => new CreateOptionDto {
                        Name = o.Name,
                        Price = o.Price
                    }).ToList()
                }).ToList()
            );

            await _createServiceUseCase.ExecuteAsync(command);
            return OkResponse(Messages.Service.Created);
        }

    }
}
