namespace Manzili.Api.DTOs.Admin
{
    public class RejectPaymentProofRequestDto
    {
        public int TransactionId { get; set; }
        public string RejectionReason { get; set; } = string.Empty;
    }
}
