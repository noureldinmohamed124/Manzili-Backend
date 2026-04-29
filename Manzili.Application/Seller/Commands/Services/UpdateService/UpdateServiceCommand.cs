using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manzili.Application.Seller.Commands.Services.UpdateService
{
    public record UpdateServiceCommand(
        int ServiceId,
        string Title,
        string Description,
        int CategoryId,
        decimal BasePrice,
        IReadOnlyList<string> Images,
        IReadOnlyList<UpdateOptionGroupCommand> OptionGroups
    );

    public class UpdateOptionGroupCommand
    {
        public string Name { get; set; } = string.Empty;
        public bool IsRequired { get; set; }
        public IReadOnlyList<UpdateOptionCommand> Options { get; set; } = new List<UpdateOptionCommand>();
    }
    public class UpdateOptionCommand
    {
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }
}
