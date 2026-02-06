using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manzili.Application.Commands.Auth.LoginUser
{
    public record LoginUserCommand(string Email, string Password);
}
