using Manzili.Application.Seller.UseCases;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Manzili.Api.Controllers.Seller
{
    [Route("api/[controller]")]
    [ApiController]
    public class SellerController : BaseApiController
    {
        private readonly GetDashboardStatsUseCase _getDashboardStatsUseCase;

        public SellerController(GetDashboardStatsUseCase getDashboardStatsUseCase)
        {
            _getDashboardStatsUseCase = getDashboardStatsUseCase;
        }

        [HttpGet("dashboard")]
        public async Task<IActionResult> GetDashboardStats()
        {
            var result = await _getDashboardStatsUseCase.ExecuteAsync();

            return OkResponse(result);
        }
    }
}
