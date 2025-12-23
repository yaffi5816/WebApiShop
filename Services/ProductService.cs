using AutoMapper;
using DTOs;
using Entities;
using Repositories;
using System.Text.Json;
namespace Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;// = new UserRepository();
        private readonly IMapper _mapper;
        public ProductService(IProductRepository repository,IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductDTO>> GetAsync(int position, int skip, string? name, int? minPrice, int? maxPrice, int?[] categoriesId)
        {
            IEnumerable<Product> products = await _repository.GetAsync(position, skip, name, minPrice, maxPrice, categoriesId);
            IEnumerable<ProductDTO> productsDTO = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(products);
            return productsDTO;
            
        }

 
    }
}
