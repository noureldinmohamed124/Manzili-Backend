using Manzili.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Manzili.Application.Common.Expressions
{
    public static class UserExpressions
    {
        public static Expression<Func<User, bool>> IsCurrentlyBlocked()
        {
            return u =>
                u.IsBlocked && (u.BlockedUntil == null || u.BlockedUntil > DateTime.UtcNow);
        }
    }
}
