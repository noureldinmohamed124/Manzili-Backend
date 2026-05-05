using Manzili.Application.Admin.Dashboard;
using Manzili.Application.Seller.Queries.Services.GetDashboardStats;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Manzili.Api.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminController : BaseApiController
    {
        private readonly GetAdminDashboardStatsUseCase _getAdminDashboardStatsUseCase;

        public AdminController(GetAdminDashboardStatsUseCase getAdminDashboardStatsUseCase)
        {
            _getAdminDashboardStatsUseCase = getAdminDashboardStatsUseCase;
        }

        [HttpGet("dashboard")]
        public async Task<IActionResult> GetDashboardStats()
        {
            var query = new GetDashboardStatsQuery();

            var result = await _getAdminDashboardStatsUseCase.ExecuteAsync(query);

            return Ok(result);
        }
    }
}
