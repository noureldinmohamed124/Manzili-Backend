using Manzili.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manzili.Application.Common.Extensions
{
    public static class ServiceStatusEnumExtension
    {
        public static int ToId(this ServiceStatus status)
        => (int)status;

        public static string ToString(this ServiceStatus status)
            => status.ToString();

    }
}
