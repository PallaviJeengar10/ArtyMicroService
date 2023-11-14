using Arty.Dtos;
using SharedModels.Models;

namespace Catalog.Services.Categories
{
    public interface ICategoryService
    {
        public Task<List<Category>> GetCategories();
        public Task<int> AddCategory(CategoryDto newCategory);
        public Task<Boolean> UpdateCategory(int categoryId, CategoryDto updatecategoryRequest);
        public Task DeleteCategory(List<SubCategory> subCategories, int categoryId);
    }
}
