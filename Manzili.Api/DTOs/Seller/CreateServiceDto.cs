using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manzili.Api.DTOs.Seller
{
    public class CreateServiceDto
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public decimal BasePrice { get; set; }
        public List<IFormFile> Images { get; set; } = new List<IFormFile>();
        public List<CreateServiceOptionGroupDto> OptionGroups { get; set; } = new List<CreateServiceOptionGroupDto>();
    }

    public class CreateServiceOptionGroupDto
    {
        public string Name { get; set; }= string.Empty;
        public bool IsRequired { get; set; }
        public List<CreateServiceOptionDto> Options{ get; set; } = new List<CreateServiceOptionDto>();
    }

    public class CreateServiceOptionDto
    {
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }
}
