using Manzili.Application.Abstractions.Persistence;
using Manzili.Application.Abstractions.Security;
using Manzili.Application.Auth.Commands;
using Manzili.Application.Auth.Commands.RefreshToken;
using Manzili.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Manzili.Application.Auth.UseCases
{
    public class RefreshTokenUseCase
    {
        private readonly IRefreshTokenRepo _refreshTokenRepo;
        private readonly IRefreshTokenService _refreshTokenService;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IUserRepo _userRepo;

        public RefreshTokenUseCase(IRefreshTokenRepo refreshTokenRepo, IRefreshTokenService refreshTokenService, IJwtTokenService jwtTokenService, IUserRepo userRepo)
        {
            _refreshTokenRepo = refreshTokenRepo;
            _refreshTokenService = refreshTokenService;
            _jwtTokenService = jwtTokenService;
            _userRepo = userRepo;
        }

        public async Task<AuthTokenDto> ExecuteAsync(RefreshTokenCommand command)
        {
            var oldRefreshTokenHash = _refreshTokenService.HashRefreshToken(command.RefreshToken);

            var newRefreshToken = _refreshTokenService.Generate();
            var newHash = _refreshTokenService.HashRefreshToken(newRefreshToken);

            var newExpireDate = DateTime.UtcNow.AddDays(14);

            var rows = await _refreshTokenRepo.RotateAsync(
                oldRefreshTokenHash,
                newHash,
                newExpireDate,
                command.IpAddress,
                command.DeviceInfo
            );

            if (rows == 0)
                throw new SecurityException("Invalid or reused refresh token");

            var refreshtoken = await _refreshTokenRepo.GetByHashAsync(newHash);

            var user = await _userRepo.GetByIdAsync(refreshtoken!.UserId)
                ?? throw new UnauthorizedException("User not found");


            if (user.IsBlocked &&
                user.BlockedUntil.HasValue &&
                user.BlockedUntil > DateTime.UtcNow)
            {
                throw new UnauthorizedException("User account is blocked");
            }

            // Generate new tokens
            var newAccessToken = _jwtTokenService.GenerateToken(user);


            return new AuthTokenDto
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken
            };

        }
    }
}
