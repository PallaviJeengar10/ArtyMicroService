using Arty.Dtos;
using Arty.Models;
using Microsoft.EntityFrameworkCore;
using SharedModels.Models;

namespace Catalog.DataRepositories.Categories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ArtyContext _context;
        public CategoryRepository(ArtyContext context)
        {
            _context = context;
        }
        public async Task<List<Category>> GetCategories()
        {
             return await _context.Categories.ToListAsync();
        }

        public async Task<int> AddCategory(Category category)
        {
            await _context.Categories.AddAsync(category);
            _context.SaveChanges();
            return category.CategoryId;
        }

        public async Task UpdateCategory(Category category, CategoryDto updatecategoryRequest)
        {
            category.CategoryName = updatecategoryRequest.CategoryName;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCategory(List<SubCategory> subCategories, Category category)
        {
            _context.SubCategories.RemoveRange(subCategories);
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }

        public async Task<Category?> GetCategoryById(int categoryId)
        {
            return await _context.Categories.FindAsync(categoryId);
        }
    }
}
