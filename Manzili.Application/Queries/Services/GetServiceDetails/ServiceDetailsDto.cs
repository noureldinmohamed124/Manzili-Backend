using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Manzili.Application.Queries.Services.GetServiceDetails
{
    public class ServiceDetailsDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string ServiceDescription { get; set; } = null!;
        public decimal BasePrice { get; set; }
        public string Address { get; set; } = null!;
        public Provider Provider { get; set; } = null!;
        public List<OptionsList> Options { get; set; } = new List<OptionsList>();
        public List<Image> Images { get; set; } = new List<Image>();
    }
    public class OptionsList
    {
        public int Id { get; set; }
        public string ServiceOptionName { get; set; } = null!;
        public decimal Price { get; set; }
    }
    public class Provider
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public decimal rating { get; set; }
        public int ReviewsNo { get; set; }
    }
    public class Image
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; } = null!;
    }
}
