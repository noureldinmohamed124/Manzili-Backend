using Manzili.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manzili.Application.Admin.Users.Queries.GetAdminAllUsers
{
    public class AdminUserDto
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string ProfilePicture { get; set; } = string.Empty;

        public UserRole Role { get; set; }

        public bool IsBlocked { get; set; }
        public DateTime? BlockedUntil { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
