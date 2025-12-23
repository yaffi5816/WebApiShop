using DTOs;
using Entities;

namespace Services
{
    public interface IProductService
    {

        Task<IEnumerable<ProductDTO>> GetAsync(int position, int skip, string? name, int? minPrice, int? maxPrice, int?[] categoriesId);
    }
}