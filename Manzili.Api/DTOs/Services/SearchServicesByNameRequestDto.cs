using System.ComponentModel.DataAnnotations;

namespace Manzili.Api.DTOs.Services
{
    public class SearchServicesByNameRequestDto
    {
        [Required]
        public string Keyword {  get; set; } = string.Empty;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
