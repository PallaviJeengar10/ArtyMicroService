using Arty.Dtos;
using AutoMapper;
using Catalog.DataRepositories.Categories;
using SharedModels.Models;

namespace Catalog.Services.Categories
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;
        public CategoryService(ICategoryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<int> AddCategory(CategoryDto newCategory)
        {
            Category category = _mapper.Map<Category>(newCategory);
            return await _repository.AddCategory(category);
        }

        public async Task DeleteCategory(List<SubCategory> subCategories, int categoryId)
        {
            var category = await _repository.GetCategoryById(categoryId);
            if (category != null)
            {
                await _repository.DeleteCategory(subCategories, category);
            }
        }

        public Task<List<Category>> GetCategories()
        {
            return _repository.GetCategories();
        }

        public async Task<Boolean> UpdateCategory(int categoryId, CategoryDto updatecategoryRequest)
        {
            var category = await _repository.GetCategoryById(categoryId);
            if (category != null)
            {
                await _repository.UpdateCategory(category, updatecategoryRequest);
                return true;
            }
            return false;
        }
    }
}
