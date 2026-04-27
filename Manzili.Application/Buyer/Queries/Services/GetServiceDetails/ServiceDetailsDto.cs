using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Manzili.Application.Buyer.Queries.Services.GetServiceDetails
{
    public class ServiceDetailsDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string ServiceDescription { get; set; } = null!;
        public decimal BasePrice { get; set; }
        public string Address { get; set; } = null!;
        public Provider Provider { get; set; } = null!;
        public List<OptionGroupDto> OptionGroups { get; set; } = new();
        public List<ImageDto> Images { get; set; } = new();
    }
    public class OptionGroupDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public bool IsRequired { get; set; }
        public bool AllowMultiple { get; set; }

        public List<OptionsListDto> Options { get; set; } = new();
    }

    public class OptionsListDto
    {
        public int Id { get; set; }
        public string ServiceOptionName { get; set; } = null!;
        public decimal? PriceAdjustment { get; set; }
    }
    public class Provider
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public decimal rating { get; set; }
        public int ReviewsNo { get; set; }
    }
    public class ImageDto
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; } = null!;
    }
}
