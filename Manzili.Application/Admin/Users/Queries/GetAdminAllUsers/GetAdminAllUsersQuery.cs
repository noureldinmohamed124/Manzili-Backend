using Manzili.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manzili.Application.Admin.Users.Queries.GetAdminAllUsers
{
    public class GetAdminAllUsersQuery
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        public UserRole? Role { get; set; }
        public bool? IsBlocked { get; set; }

        public string? Search { get; set; }
    }
}
