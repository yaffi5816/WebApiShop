using DTO;

namespace Services
{
    public interface IProductService
    {
        Task<ProductDTO> AddProduct(PostProductDTO product);
        Task<ProductDTO> GetProductById(int id);
        Task<PageResponseDTO<ProductDTO>> GetProducts(int position, int skip, int?[] categoryIds, string? description, int? maxPrice, int? minPrice);
    }
}