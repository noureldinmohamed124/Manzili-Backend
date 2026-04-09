using System.ComponentModel.DataAnnotations;

namespace Manzili.Api.DTOs.Orders
{
    public class RePriceOrderDto
    {
        [Required]
        public int? TransactionId { get; set; }

        [Required]
        public decimal NewPrice { get; set; }
        [Required]
        public string Reason { get; set; } = string.Empty;
    }
}
