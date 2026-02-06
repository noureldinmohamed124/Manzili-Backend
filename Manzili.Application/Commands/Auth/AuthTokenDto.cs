using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manzili.Application.Commands.Auth
{
    public class AuthTokenDto
    {
        public string AccessToken { get; set; } = null!;
        public string? RefreshToken { get; set; }
    }
}
