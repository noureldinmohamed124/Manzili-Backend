using Manzili.Domain.Enums;

namespace Manzili.Api.DTOs.Admin
{
    public class GetAdminAllUsersRequestDto
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        public UserRole? Role { get; set; }
        public bool? IsBlocked { get; set; }

        public string? Search { get; set; }
    }
}
