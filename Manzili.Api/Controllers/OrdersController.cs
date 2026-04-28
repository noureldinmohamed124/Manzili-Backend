using Manzili.Api.Common;
using Manzili.Api.DTOs.Orders;
using Manzili.Application.Buyer.Commands.Orders;
using Manzili.Application.Buyer.Commands.Orders.SubmitPayment;
using Manzili.Application.Buyer.Queries.Orders.GetAllOrders;
using Manzili.Application.Buyer.Queries.Orders.GetPaymentSummary;
using Manzili.Application.Buyer.UseCases.Orders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Manzili.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Provider")]
    public class OrdersController : BaseApiController
    {
        private readonly RequestServiceUseCase _requestServiceUseCase;
        private readonly GetAllOrdersUseCase _getAllOrdersUseCase;
        private readonly GetPaymentSummaryUseCase _getPaymentSummaryUseCase;
        private readonly SubmitPaymentUseCase _submitPaymentUseCase;

        public OrdersController(RequestServiceUseCase requestServiceUseCase, GetAllOrdersUseCase getAllOrdersUseCase, GetPaymentSummaryUseCase getPaymentSummaryUseCase, SubmitPaymentUseCase submitPaymentUseCase)
        {
            _requestServiceUseCase = requestServiceUseCase;
            _getAllOrdersUseCase = getAllOrdersUseCase;
            _getPaymentSummaryUseCase = getPaymentSummaryUseCase;
            _submitPaymentUseCase = submitPaymentUseCase;
        }


        // by the buyer to get the approved requests from the seller (waiting for payment)
        [HttpGet]
        public async Task<IActionResult> GetAllOrders([FromQuery]GetOrdersRequestDto dto)
        {
            var command = new GetOrdersQuery(
                Status: dto.status,
                Page: dto.Page,
                PageSize: dto.PageSize
            );

            var orders = await _getAllOrdersUseCase.ExecuteAsync(command);

            return OkResponse(orders);
        }


        // by the buyer (Order a service)
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

            return OkResponse(new { orderId = orderId }, Messages.Order.Created);
        }


        // get the payment summary before procced to payment screen
        [HttpGet("payment-summary")]
        public async Task<IActionResult> GetOrderPaymentSummary(GetPaymentSummaryRequestDto dto)
        {
            var query = new GetPaymentSummaryQuery(
                OrderIds: dto.OrderIds
            );
            var summary = await _getPaymentSummaryUseCase.ExecuteAsync(query);
            return OkResponse(summary);
        }


        // submit payment
        [HttpPost("submit-payment")]
        public async Task<IActionResult> SubmitPayment(SubmitPaymentRequestDto dto)
        {
            var command = new SubmitPaymentCommand(
                OrderIds: dto.OrderIds,
                PaymentScreenshot: dto.PaymentScreenshot,
                Notes: dto.Notes
            );
            var result = await _submitPaymentUseCase.ExecuteAsync(command);
            return OkResponse(result);
        }


        //// accept the service request from the buyer
        //[HttpPost("accept")]
        //public async Task<IActionResult> AcceptTheOrder()
        //{

        //    return OkResponse("");
        //}


        //// Reprice the Request by the provider
        //[HttpPost("reprice")]
        //public async Task<IActionResult> RepriceTheOrder(RePriceOrderDto dto)
        //{

        //    return OkResponse("");
        //}


        //// accept the RePrice by the buyer
        //[HttpPost("accept-price")]
        //public async Task<IActionResult> AcceptTheRequestRePrice()
        //{

        //    return OkResponse("");
        //}
    }
}
