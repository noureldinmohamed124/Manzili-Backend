using Manzili.Api.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Manzili.Api.Controllers
{
    [ApiController]
    public abstract class BaseApiController : ControllerBase
    {
        protected IActionResult OkResponse<T>(T? data, string? message = null)
            => Ok(ApiResponse<T>.Ok(data, message));

        protected IActionResult OkResponse(string message)
            => Ok(ApiResponse<object>.Ok(null, message));
    }
}
