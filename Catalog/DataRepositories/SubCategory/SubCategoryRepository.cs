using Arty.Models;
using Microsoft.EntityFrameworkCore;
using SharedModels.Models;

namespace Catalog.DataRepositories.SubCategories
{
    public class SubCategoryRepository : ISubCategoryRepository
    {
        private readonly ArtyContext _context;
        public SubCategoryRepository(ArtyContext context)
        {
            _context = context;
        }

        public async Task<int> AddSubCategory(SubCategory subCategory)
        {
            await _context.SubCategories.AddAsync(subCategory);
            _context.SaveChanges();
            return subCategory.SubCategoryId;
        }

        public async Task DeleteSubCategory(List<Product> products, SubCategory subCategory)
        {
            _context.Products.RemoveRange(products);
            _context.SubCategories.Remove(subCategory);
            await _context.SaveChangesAsync();
        }

        public async Task<List<SubCategory>> GetSubCategories()
        {
            return await _context.SubCategories.ToListAsync();
        }

        public async Task<List<SubCategory>> GetSubCategoriesByCategoryId(int categoryId)
        {
            return await _context.SubCategories
                    .Where(c => c.CategoryId == categoryId)
                    .ToListAsync();
        }

        public async Task<SubCategory> GetSubCategoryById(int subCategoryId)
        {
            var subCategory = await _context.SubCategories.FindAsync(subCategoryId);
            return subCategory ?? new SubCategory();
        }

        public async Task UpdateSubCategory(SubCategory subCategory, SubCategory updateSubCategoryRequest)
        {
            subCategory.SubCategoryName = updateSubCategoryRequest.SubCategoryName;
            subCategory.CategoryId = updateSubCategoryRequest.CategoryId;
            await _context.SaveChangesAsync();
        }
    }
}
