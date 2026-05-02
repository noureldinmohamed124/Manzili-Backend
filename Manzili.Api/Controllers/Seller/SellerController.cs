using Manzili.Api.Common;
using Manzili.Api.DTOs.Seller;
using Manzili.Application.Abstractions.FileStorage;
using Manzili.Application.Seller.Commands.Orders.ApproveOrder;
using Manzili.Application.Seller.Commands.Orders.RejectOrder;
using Manzili.Application.Seller.Commands.Orders.RePrice_Order;
using Manzili.Application.Seller.Commands.Orders.UpdateOrderStatus;
using Manzili.Application.Seller.Commands.Services.CreateService;
using Manzili.Application.Seller.Commands.Services.DeleteService;
using Manzili.Application.Seller.Commands.Services.UpdateService;
using Manzili.Application.Seller.Queries.Orders.GetAllSellerOrders;
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
        private readonly UpdateServiceUseCase _updateServiceUseCase;
        private readonly DeleteServiceUseCase _deleteServiceUseCase;
        private readonly GetSellerOrdersUseCase _getSellerOrdersUseCase;
        private readonly GetSellerOrderByIdUseCase _getSellerOrderByIdUseCase;
        private readonly ApproveOrderUseCase _approveOrderUseCase;
        private readonly RejectOrderUseCase _rejectOrderUseCase;
        private readonly RepriceOrderUseCase _repriceOrderUseCase;
        private readonly UpdateOrderStatusUseCase _updateOrderStatusUseCase;
        private readonly IFileStorageService _fileStorageService;

        public SellerController(GetDashboardStatsUseCase getDashboardStatsUseCase, GetSellerServicesUseCase getSellerServicesUseCase, GetSellerServiceByIdUseCase getSellerServiceByIdUseCase, CreateServiceUseCase createServiceUseCase, UpdateServiceUseCase updateServiceUseCase, IFileStorageService fileStorageService, DeleteServiceUseCase deleteServiceUseCase, GetSellerOrdersUseCase getSellerOrdersUseCase, GetSellerOrderByIdUseCase getSellerOrderByIdUseCase, ApproveOrderUseCase approveOrderUseCase, RejectOrderUseCase rejectOrderUseCase, RepriceOrderUseCase repriceOrderUseCase, UpdateOrderStatusUseCase updateOrderStatusUseCase)
        {
            _getDashboardStatsUseCase = getDashboardStatsUseCase;
            _getSellerServicesUseCase = getSellerServicesUseCase;
            _getSellerServiceByIdUseCase = getSellerServiceByIdUseCase;
            _createServiceUseCase = createServiceUseCase;
            _updateServiceUseCase = updateServiceUseCase;
            _fileStorageService = fileStorageService;
            _deleteServiceUseCase = deleteServiceUseCase;
            _getSellerOrdersUseCase = getSellerOrdersUseCase;
            _getSellerOrderByIdUseCase = getSellerOrderByIdUseCase;
            _approveOrderUseCase = approveOrderUseCase;
            _rejectOrderUseCase = rejectOrderUseCase;
            _repriceOrderUseCase = repriceOrderUseCase;
            _updateOrderStatusUseCase = updateOrderStatusUseCase;
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
        public async Task<IActionResult> CreateService([FromForm] CreateServiceDto dto)
        {
            var imageUrls = new List<string>();

            foreach (var image in dto.Images)
            {
                using var stream = image.OpenReadStream();
                var imageUrl = await _fileStorageService.SaveImageAsync(stream, image.FileName, "services");

                imageUrls.Add(imageUrl);
            }

            var command = new CreateServiceCommand(
                Title: dto.Title,
                Description: dto.Description,
                CategoryId: dto.CategoryId,
                BasePrice: dto.BasePrice,
                Images: imageUrls,
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


        [HttpPut("services/{id}")]
        public async Task<IActionResult> UpdateService(int id, [FromForm] UpdateServiceDto dto)
        {

            // 1. Upload images first
            var imageUrls = new List<string>();

            if (dto.Images != null && dto.Images.Any())
            {
                foreach (var image in dto.Images)
                {
                    using var stream = image.OpenReadStream();

                    var imageUrl = await _fileStorageService
                        .SaveImageAsync(stream, image.FileName, "services");

                    imageUrls.Add(imageUrl);
                }
            }

            var command = new UpdateServiceCommand(
                ServiceId: id,
                Title: dto.Title,
                Description: dto.Description,
                CategoryId: dto.CategoryId,
                BasePrice: dto.BasePrice,
                Images: imageUrls,
                OptionGroups: dto.OptionGroups
                    .Select(g => new UpdateOptionGroupCommand
                    {
                        Name = g.Name,
                        IsRequired = g.IsRequired,
                        Options = g.Options
                            .Select(o => new UpdateOptionCommand
                            {
                                Name = o.Name,
                                Price = o.Price
                            }).ToList()
                    }).ToList()
            );

            await _updateServiceUseCase.ExecuteAsync(command);

            return OkResponse(Messages.Service.Updated);
        }


        [HttpDelete("services/{id}")]
        public async Task<IActionResult> DeleteService(int id)
        {
            var command = new DeleteServiceCommand(id);

            await _deleteServiceUseCase.ExecuteAsync(command);

            return OkResponse(Messages.Service.Deleted);
        }


        // Orders Actions Endpoints
        [HttpGet("orders")]
        public async Task<IActionResult> GetAllSellerOrders([FromQuery] GetAllSellerOrdersDto dto)
        {
            var query = new GetSellerOrdersQuery(
                Status: dto.Status,
                Page: dto.Page ?? 1,
                PageSize: dto.PageSize ?? 10
            );
            
            var result =  await _getSellerOrdersUseCase.ExecuteAsync(query);
            return OkResponse(result);
        }


        [HttpGet("orders/{id}")]
        public async Task<IActionResult> GetSellerOrderDetailsById(int id)
        {
            var result = await _getSellerOrderByIdUseCase.ExecuteAsync(id);
            return OkResponse(result);
        }


        [HttpPost("orders/{id}/approve")]
        public async Task<IActionResult> ApproveOrder(int id)
        {
            await _approveOrderUseCase.ExecuteAsync(new ApproveOrderCommand(id));

            return OkResponse(Messages.Order.Approved);
        }


        [HttpPost("orders/{id}/reject")]
        public async Task<IActionResult> RejectOrder(int id, RejectOrderDto dto)
        {
            var command = new RejectOrderCommand(
                id,
                dto.Reason
            );

            await _rejectOrderUseCase.ExecuteAsync(command);

            return OkResponse(Messages.Order.Rejected);
        }


        [HttpPost("orders/{id}/reprice")]
        public async Task<IActionResult> RepriceOrder(int id, RepriceOrderDto dto)
        {
            var command = new RepriceOrderCommand(
                id,
                dto.NewPrice,
                dto.Reason
            );

            await _repriceOrderUseCase.ExecuteAsync(command);

            return OkResponse(Messages.Order.Repriced);
        }



        [HttpPatch("orders/{id}/status")]
        public async Task<IActionResult> UpdateOrderStatus(int id, UpdateOrderStatusDto dto)
        {
            var command = new UpdateOrderStatusCommand(
                id,
                dto.Status
            );
            await _updateOrderStatusUseCase.ExecuteAsync(command);

            return OkResponse(Messages.Order.StatusUpdated(dto.Status.ToString()));
        }

    }
}
