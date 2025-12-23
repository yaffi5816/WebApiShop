using Entities;
using Services;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using System.Text.Json;
using DTO;
//using WebApiShop.Properties;

namespace WebApiShop.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductsController(IProductService service)
        {
            _service = service;
        }
        

        // GET api/<Users>/5
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> Get(int position, int skip, string? name, int? minPrice, int? maxPrice, int?[] categoriesId)
        {
            IEnumerable<ProductDTO> products=await _service.GetAsync(position, skip, name, minPrice, maxPrice, categoriesId);
            if (products != null)
            {
                return Ok(products);
            }
            return NoContent();
        }

        
    }
}
