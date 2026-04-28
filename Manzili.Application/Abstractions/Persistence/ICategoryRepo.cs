using Manzili.Application.Shared.Queries.GetCategories;
using Manzili.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manzili.Application.Abstractions.Persistence
{
    public interface ICategoryRepo : IGenericRepo<Category>
    {
        public Task<CategoriesListDto> GetAllCategoriesAsync();
    }
}
