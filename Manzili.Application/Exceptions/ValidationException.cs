using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manzili.Application.Exceptions
{
    public sealed class ValidationException : AppException
    {
        public IReadOnlyDictionary<string, string[]> Errors { get; }

        public ValidationException(IReadOnlyDictionary<string, string[]> errors, string message = "Validation failed") : base(message)
        {
            Errors = errors;
        }
    }
}
