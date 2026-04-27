using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manzili.Application.Buyer.Commands.Orders
{
    public record RequestServiceCommand(
        int ServiceId,
        string? CustomizationText,
        string? CustomRequestImage,
        int Quantity,
        IReadOnlyList<SelectedOptionGroup> OptionGroups
    );

    public record SelectedOptionGroup(
        int GroupId,
        IReadOnlyList<OptionItem> Options
    );

    public record OptionItem(
        int OptionId,
        int Quantity
    );
}
