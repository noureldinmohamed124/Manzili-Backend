using Manzili.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manzili.Application.Admin.Users.Queries.GetAdminUserById
{
    public class AdminUserDetailsDto
    {
        public int Id { get; set; }

        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        public string? ImageUrl { get; set; }

        public UserRole Role { get; set; }

        // Blocking
        public bool IsBlocked { get; set; }
        public DateTime? BlockedUntil { get; set; }
        public string? BlockReason { get; set; }
        public string? BlockedByAdmin { get; set; } = string.Empty;

        // Audit
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // Stats (VERY useful)
        public int ServicesCount { get; set; }
        public int OrdersCount { get; set; }
    }
}
