namespace Manzili.Api.DTOs.Services
{
    public class GetServicesRequestDto
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int? CategoryId { get; set; }
        public bool? IsRecommended { get; set; }
        public bool? TopDiscounts { get; set; }
        public bool? MostPurchased { get; set; }
    }
}
