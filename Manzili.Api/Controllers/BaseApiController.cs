using Manzili.Api.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Manzili.Api.Controllers
{
    [ApiController]
    public abstract class BaseApiController : ControllerBase
    {
        protected ActionResult<ApiResponse<T>> OkResponse<T>(T? data, string? message = null)
        => Ok(ApiResponse<T>.Ok(data, message));

        protected ActionResult<ApiResponse<T>> FailResponse<T>(string message, int statusCode)
            => StatusCode(statusCode, ApiResponse<T>.Fail(message));
    }
}
