using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manzili.Application.Auth.Commands.LoginUser
{
    public record LoginUserCommand(string Email, string Password, string? IpAddress, string? DeviceInfo);
}
