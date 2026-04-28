using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manzili.Application.Seller.Commands.Services.CreateService
{
    public record CreateServiceCommand(
        string Title,
        string Description,
        int CategoryId,
        decimal BasePrice,
        IReadOnlyList<string> Images,
        IReadOnlyList<CreateOptionGroupDto> OptionGroups
    );
    public class CreateOptionGroupDto
    {
        public string Name { get; set; } = string.Empty;
        public bool IsRequired { get; set; }
        public IReadOnlyList<CreateOptionDto> Options { get; set; } = new List<CreateOptionDto>();
    }
    public class CreateOptionDto
    {
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }
}
