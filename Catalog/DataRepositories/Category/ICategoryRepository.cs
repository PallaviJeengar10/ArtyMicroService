using Arty.Dtos;
using SharedModels.Models;

namespace Catalog.DataRepositories.Categories
{
    public interface ICategoryRepository
    {
        public Task<List<Category>> GetCategories();
        public Task<int> AddCategory(Category category);
        public Task UpdateCategory(Category category, CategoryDto updatecategoryRequest);
        public Task DeleteCategory(List<SubCategory> subCategories, Category category);
        public Task<Category?> GetCategoryById(int categoryId);
    }
}
