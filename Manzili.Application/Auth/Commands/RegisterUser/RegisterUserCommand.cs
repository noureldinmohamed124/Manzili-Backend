using Manzili.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manzili.Application.Auth.Commands.RegisterUser
{
    public record RegisterUserCommand(
        string FullName,
        string Email,
        string Password,
        UserRole Role
    );
}
