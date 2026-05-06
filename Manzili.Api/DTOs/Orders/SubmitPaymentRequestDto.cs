namespace Manzili.Api.DTOs.Orders
{
    public class SubmitPaymentRequestDto
    {
        public List<int> OrderIds { get; set; } = new List<int>();
        public IFormFile PaymentScreenshot { get; set; } = null!;
        public string? Notes { get; set; }
    }
}
