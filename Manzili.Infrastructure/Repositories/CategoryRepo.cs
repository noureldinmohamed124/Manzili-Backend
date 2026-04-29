using Manzili.Application.Abstractions.Persistence;
using Manzili.Application.Shared.Queries.GetCategories;
using Manzili.Domain.Entities;
using Manzili.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manzili.Infrastructure.Repositories
{
    public class CategoryRepo : GenericRepo<Category>, ICategoryRepo
    {
        public CategoryRepo(ManziliDbContext context) : base(context)
        {
        }

        public async Task<bool> ExistsAsync(int categoryId)
        {
            var cat = await _context.Services.FindAsync(categoryId);

            bool exists = false;
            if (cat != null)
                exists = true;

            return exists;
        }

        public async Task<CategoriesListDto> GetAllCategoriesAsync()
        {
            var categories = await _context.Categories
                .Select(c => new CategoryItem
                {
                    Id = c.Id,
                    NameAr = c.NameAr,
                    CreatedAt = c.CreatedAt,
                    IsActive = c.IsActive,
                    Slug = c.Slug,
                    SortOrder = c.SortOrder
                }).ToListAsync();

            var categoriesList = new CategoriesListDto
            {
                Items = categories
            };

            return categoriesList;
        }

    }
}
