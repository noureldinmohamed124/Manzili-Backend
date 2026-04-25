namespace Manzili.Api.DTOs.Orders
{
    public class GetPaymentSummaryRequestDto
    {
        public List<int> OrderIds { get; set; } = new List<int>();
    }
}
