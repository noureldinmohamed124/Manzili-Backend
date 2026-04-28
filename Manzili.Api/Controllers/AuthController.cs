using Manzili.Api.DTOs.Auth;
using Manzili.Application.Auth.Commands.LoginUser;
using Manzili.Application.Auth.Commands.RefreshToken;
using Manzili.Application.Auth.Commands.RegisterUser;
using Manzili.Application.Auth.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace Manzili.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseApiController
    {

        private readonly RegisterUserUseCase _registerUserUseCase;
        private readonly LoginUserUseCase _loginUserUseCase;
        private readonly RefreshTokenUseCase _refreshTokenUseCase;

        public AuthController(RegisterUserUseCase registerUserUseCase, LoginUserUseCase loginUserUseCase, RefreshTokenUseCase refreshTokenUseCase)
        {
            _registerUserUseCase = registerUserUseCase;
            _loginUserUseCase = loginUserUseCase;
            _refreshTokenUseCase = refreshTokenUseCase;
        }


        // Register
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequestDto dto)
        {
            var command = new RegisterUserCommand(
                FullName: dto.FullName,
                Email: dto.Email,
                Password: dto.Password,
                Role: dto.Role
            );

            await _registerUserUseCase.ExecuteAsync(command);

            return OkResponse("User Registered Successfully");
        }


        // Login
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDto dto)
        {
            var command = new LoginUserCommand(
                Email: dto.Email,
                Password: dto.Password,
                IpAddress: HttpContext.Connection.RemoteIpAddress?.ToString(),
                DeviceInfo: Request.Headers["User-Agent"].ToString()
            );

            var result = await _loginUserUseCase.ExecuteAsync(command);

            return OkResponse(result, "Login Successfully");
        }


        // Refresh
        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh(RefreshTokenRequestDto dto)
        {
            var command = new RefreshTokenCommand(
                RefreshToken: dto.RefreshToken,
                IpAddress: HttpContext.Connection.RemoteIpAddress?.ToString(),
                DeviceInfo: Request.Headers["User-Agent"].ToString()
            );

            var result = await _refreshTokenUseCase.ExecuteAsync(command);

            return OkResponse(result);
        }
    }
}
