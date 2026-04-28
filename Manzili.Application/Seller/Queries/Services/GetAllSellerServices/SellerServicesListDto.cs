using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manzili.Application.Seller.Queries.Services.GetAllSellerServices
{
    public class SellerServicesListDto
    {
        public List<SellerServiceItemDto> Items { get; set; } = new List<SellerServiceItemDto>();
    }

    public class SellerServiceItemDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public decimal Rating { get; set; }
        public int OrdersCount { get; set; }
        public string Status { get; set; } = string.Empty;
        public decimal BasePrice { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
