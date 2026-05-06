namespace Manzili.Api.DTOs.Admin
{
    public class GetAdminFinancialsRequestDto
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        public DateTime? From { get; set; }
        public DateTime? To { get; set; }

        public int? BuyerId { get; set; }
        public int? ProviderId { get; set; }
    }
}
