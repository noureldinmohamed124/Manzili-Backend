using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manzili.Application.Auth.Commands.RefreshToken
{
    public record RefreshTokenCommand(string RefreshToken, string? IpAddress, string? DeviceInfo);
}
