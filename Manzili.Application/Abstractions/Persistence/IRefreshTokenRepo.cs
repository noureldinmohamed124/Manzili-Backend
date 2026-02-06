using Manzili.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manzili.Application.Abstractions.Persistence
{
    public interface IRefreshTokenRepo : IGenericRepo<RefreshToken>
    {
        Task<RefreshToken?> GetByHashAsync(string tokenHash);
        Task<RefreshToken?> GetByUserIdAsync(int userId);
        Task<int> RotateAsync(
        string oldTokenHash,
        string newTokenHash,
        DateTime newExpiry,
        string? ipAddress,
        string? deviceInfo);
    }
}
