using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manzili.Application.Shared.Queries.GetCategories
{
    public class CategoriesListDto
    {
        public IReadOnlyList<CategoryItem> Items { get; set; } = new List<CategoryItem>();
    }

    public class CategoryItem
    {
        public int Id { get; set; }
        public string Slug { get; set; } = string.Empty;
        public string NameAr { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public int SortOrder { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
