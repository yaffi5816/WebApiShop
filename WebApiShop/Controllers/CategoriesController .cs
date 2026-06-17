using Services;
using Microsoft.AspNetCore.Mvc;
using DTO;

namespace WebApiShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Produces("application/json")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> Get()
        {
            IEnumerable<CategoryDTO> categories = await _categoryService.GetCategories();
            if (categories.Count() > 0)
                return Ok(categories);
            return NoContent();
        }
    }
}
