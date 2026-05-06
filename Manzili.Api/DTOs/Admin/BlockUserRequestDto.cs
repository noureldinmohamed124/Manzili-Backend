namespace Manzili.Api.DTOs.Admin
{
    public class BlockUserRequestDto
    {
        public string Reason { get; set; } = string.Empty;
        public DateTime? BlockedUntil { get; set; } // null = permanent
    }
}
