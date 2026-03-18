namespace Manzili.Api.DTOs.Orders
{
    public class RequestServiceDto
    {
        public int ServiceId { get; set; }
        public string? CustomizationText { get; set; }
        public string? CustomRequestImage { get; set; }
        public int Quantity { get; set; } = 1;
        public List<SelectedOptionGroupDto> OptionGroups { get; set; } = new List<SelectedOptionGroupDto>();
    }

    public class SelectedOptionGroupDto
    {
        public int GroupId { get; set; }
        public List<OptionItemDto> Items { get; set; } = new List<OptionItemDto>();
    }

    public class OptionItemDto
    {
        public int OptionId { get; set; }
        public int Quantity { get; set; } = 1;
    }
}
