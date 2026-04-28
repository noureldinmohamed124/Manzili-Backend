using Manzili.Application.Abstractions.Persistence;
using Manzili.Application.Shared.Queries.GetCategories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manzili.Application.Shared.UseCases
{
    public class GetAllCategoriesUseCase
    {
        private readonly ICategoryRepo _categoryRepo;

        public GetAllCategoriesUseCase(ICategoryRepo categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }

        public Task<CategoriesListDto> ExecuteAsync()
        {
            return _categoryRepo.GetAllCategoriesAsync();
        }
    }
}
