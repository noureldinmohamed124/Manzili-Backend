using Manzili.Application.Shared.UseCases;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Manzili.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : BaseApiController
    {
        private readonly GetAllCategoriesUseCase _getAllCategoriesUseCase;

        public CategoriesController(GetAllCategoriesUseCase getAllCategoriesUseCase)
        {
            _getAllCategoriesUseCase = getAllCategoriesUseCase;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _getAllCategoriesUseCase.ExecuteAsync();
            return OkResponse(categories);
        }
    }
}
