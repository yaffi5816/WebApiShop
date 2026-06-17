using Entities;

namespace Repositories
{
    public interface IProductRepository
    {
        Task<Product> AddProduct(Product newProduct);
        Task<Product> GetProductById(int id);
        Task<(List<Product> Items, int TotalCount)> GetProducts(int position, int skip, int?[] categoryIds, string? description, int? maxPrice, int? minPrice);
    }
}