using Entities;
using Repositories;
using System.Text.Json;
namespace Services
{
    public class CategoryService :  ICategoryService
    {
        private readonly ICategoryRepository _repository;
        public CategoryService(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Category>> GetAsync()
        {
            return await _repository.GetAsync();
        }
    }
}
