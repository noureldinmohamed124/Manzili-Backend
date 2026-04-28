using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manzili.Application.Seller.Queries.Services.GetSellerServiceById
{
    public class SellerServiceDetailsDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal BasePrice { get; set; }
        public string Category { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public decimal Rating { get; set; }
        public int OrdersCount { get; set; }
        public List<string> Images { get; set; } = [];
        public List<ServiceOptionGroupDto> OptionGroups { get; set; } = new List<ServiceOptionGroupDto>();
    }

    public class ServiceOptionGroupDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool IsRequired { get; set; }
        public List<ServiceOptionDto> Options { get; set; } = new List<ServiceOptionDto>();
    }

    public class ServiceOptionDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }
}
