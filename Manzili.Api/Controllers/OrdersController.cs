using Manzili.Api.DTOs.Orders;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Manzili.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {



        [HttpPost]
        public async Task<IActionResult> RequestAService(RequestServiceDto dto)
        {


            return Ok();
        }
    }
}
