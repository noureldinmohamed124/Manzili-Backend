using Manzili.Api.Common;
using Manzili.Api.DTOs.Orders;
using Manzili.Application.Commands.Orders;
using Manzili.Application.UseCases.Orders;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Manzili.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : BaseApiController
    {
        private readonly RequestServiceUseCase _requestServiceUseCase;

        public OrdersController(RequestServiceUseCase requestServiceUseCase)
        {
            _requestServiceUseCase = requestServiceUseCase;
        }

        [HttpPost("request")]
        public async Task<IActionResult> RequestAService(RequestServiceDto dto)
        {
            var command = new RequestServiceCommand(
                ServiceId: dto.ServiceId,
                CustomizationText: dto.CustomizationText,
                CustomRequestImage: dto.CustomRequestImage,
                Quantity: dto.Quantity,
                OptionGroups: dto.OptionGroups
                    .Select(g => new SelectedOptionGroup(
                        g.GroupId,
                        Options: g.Items.Select(o => new OptionItem(
                            o.OptionId,
                            o.Quantity
                            )).ToList()
                    )).ToList()
            );

            var orderId = await _requestServiceUseCase.ExecuteAsync(command);

            return OkResponse(orderId, Messages.Order.Created);
        }
    }
}
