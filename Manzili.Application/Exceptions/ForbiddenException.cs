using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manzili.Application.Exceptions
{
    public sealed class ForbiddenException : AppException
    {
        public ForbiddenException(string message) : base(message) { }
    }
}
