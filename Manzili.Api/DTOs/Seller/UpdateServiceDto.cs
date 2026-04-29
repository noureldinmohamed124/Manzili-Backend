namespace Manzili.Api.DTOs.Seller
{
    public class UpdateServiceDto
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public decimal BasePrice { get; set; }
        public List<IFormFile> Images { get; set; } = new List<IFormFile>();
        public List<UpdateOptionGroupDto> OptionGroups { get; set; } = new List<UpdateOptionGroupDto>();
    }
    public class UpdateOptionGroupDto
    {
        public string Name { get; set; } = string.Empty;
        public bool IsRequired { get; set; }
        public List<UpdateOptionDto> Options { get; set; }= new List<UpdateOptionDto>();
    }

    public class UpdateOptionDto
    {
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }
}
