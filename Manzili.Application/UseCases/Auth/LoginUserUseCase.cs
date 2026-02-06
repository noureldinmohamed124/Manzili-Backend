using Manzili.Application.Abstractions.Persistence;
using Manzili.Application.Abstractions.Security;
using Manzili.Application.Commands.Auth;
using Manzili.Application.Commands.Auth.LoginUser;
using Manzili.Application.Exceptions;
using Manzili.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Manzili.Application.UseCases.Auth
{
    public class LoginUserUseCase
    {
        private readonly IUserRepo _userRepo;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IRefreshTokenService _refreshTokenService;
        private readonly IRefreshTokenRepo _refreshTokenRepo;
        private readonly IUnitOfWork _unitOfWork;

        public LoginUserUseCase(IUserRepo userRepo, IPasswordHasher passwordHasher, IJwtTokenService jwtTokenService, IRefreshTokenService refreshTokenService, IRefreshTokenRepo refreshTokenRepo, IUnitOfWork unitOfWork)
        {
            _userRepo = userRepo;
            _passwordHasher = passwordHasher;
            _jwtTokenService = jwtTokenService;
            _refreshTokenService = refreshTokenService;
            _refreshTokenRepo = refreshTokenRepo;
            _unitOfWork = unitOfWork;
        }


        public async Task<AuthTokenDto> ExecuteAsync(LoginUserCommand command)
        {
            var user = await _userRepo.GetByEmailAsync(command.Email)
            ?? throw new UnauthorizedException("Invalid email or password");


            if (user.IsBlocked || (user.BlockedUntil.HasValue && user.BlockedUntil > DateTime.UtcNow))
                throw new UnauthorizedException("User account is blocked");

            if (!_passwordHasher.VerifyPassword(command.Password, user.PasswordHash))
                throw new UnauthorizedException("Invalid email or password");

            // Generate tokens
            var accessToken = _jwtTokenService.GenerateToken(user);
            var existedRefreshToken = await _refreshTokenRepo.GetByUserIdAsync(user.Id);

            string? refreshToken = null;

            if (existedRefreshToken == null)
            {
                refreshToken = _refreshTokenService.Generate();
                var hash = _refreshTokenService.HashRefreshToken(refreshToken);

                var newToken = new RefreshToken
                {
                    UserId = user.Id,
                    TokenHash = hash,
                    ExpiresAt = DateTime.UtcNow.AddDays(14),
                    IpAddress = command.IpAddress,
                    DeviceInfo = command.DeviceInfo
                };

                await _refreshTokenRepo.AddAsync(newToken);
                await _unitOfWork.SaveChangesAsync();
            }

            return new AuthTokenDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }

    }
}
