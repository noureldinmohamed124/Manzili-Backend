using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manzili.Application.Exceptions
{
    public sealed class BusinessRuleException : AppException
    {
        public BusinessRuleException(string message) : base(message) { }
    }
}
