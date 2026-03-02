namespace Manzili.Api.DTOs.Services
{
    public class RequestServiceRequestDto
    {
        public int Id { get; set; }
        public int BuyerId { get; set; }
        public string? CustomizationText { get; set; }
        public string? CustomRequestImage { get; set; }
        public int Quantity { get; set; } = 1;

        public List<OptionsGroup> Options { get; set; } = new List<OptionsGroup>();
    }

    public class OptionsGroup
    {
        public int Id { get; set; }
        public List<OptionItem> OptionItems { get; set; } = new List<OptionItem>();
    }

    public class OptionItem
    {
        public int Id { get; set; }
        public decimal? PriceAdjustment { get; set; }
        public int Quantity { get; set; } = 1;
    }
}
