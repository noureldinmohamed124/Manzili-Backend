namespace Manzili.Api.DTOs.Orders
{
    public class SubmitPaymentRequestDto
    {
        public List<int> OrderIds { get; set; } = new List<int>();
        public string PaymentScreenshot { get; set; } = string.Empty;
        public string? Notes { get; set; }
    }
}
