using Manzili.Application.Abstractions.Persistence;
using Manzili.Domain.Entities;
using Manzili.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manzili.Infrastructure.Repositories
{
    public class RefreshTokenRepo : GenericRepo<RefreshToken>, IRefreshTokenRepo
    {
        public RefreshTokenRepo(ManziliDbContext context) : base(context) { }

        public async Task<RefreshToken?> GetByHashAsync(string tokenHash)
        {
            return await _context.RefreshTokens
                .Include(rt => rt.User)
                .FirstOrDefaultAsync(rt => rt.TokenHash == tokenHash);
        }

        public async Task<RefreshToken?> GetByUserIdAsync(int userId)
        {
            return await _context.RefreshTokens
                .FirstOrDefaultAsync(rf => rf.UserId == userId);
        }

        public async Task<int> RotateAsync(string oldTokenHash, string newTokenHash, DateTime newExpiry, string? ipAddress, string? deviceInfo)
        {
            return await _context.RefreshTokens
            .Where(rt =>
                rt.TokenHash == oldTokenHash &&
                rt.RevokedAt == null &&
                rt.ExpiresAt > DateTime.UtcNow)
            .ExecuteUpdateAsync(rt => rt
                .SetProperty(x => x.TokenHash, newTokenHash)
                .SetProperty(x => x.ExpiresAt, newExpiry)
                .SetProperty(x => x.CreatedAt, DateTime.UtcNow)
                .SetProperty(x => x.IpAddress, ipAddress)
                .SetProperty(x => x.DeviceInfo, deviceInfo)
            );
        }
    }
}
