using System.ComponentModel.DataAnnotations;

namespace Manzili.Api.DTOs.Seller
{
    public class RepriceOrderDto
    {
        public decimal NewPrice { get; set; }
        [Required]
        public string Reason { get; set; } = string.Empty;
    }
}
