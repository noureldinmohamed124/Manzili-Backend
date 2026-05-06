namespace Manzili.Api.DTOs.Admin
{
    public class GetPaymentRequestsDto
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        public int? BuyerId { get; set; }
        public int? ProviderId { get; set; }

        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
    }
}
