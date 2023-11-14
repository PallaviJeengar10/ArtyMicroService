using Arty.Dtos;
using SharedModels.Models;

namespace Catalog.Services.SubCategories
{
    public interface ISubCategoryService
    {
        public Task<List<SubCategory>> GetSubCategoriesByCategoryId(int categoryId);
        public Task<List<SubCategory>> GetSubCategories();
        public Task<int> AddSubCategory(SubCategoryDto newSubCategory);
        public Task<Boolean> UpdateSubCategory(int subCategoryId, SubCategory updateSubCategoryRequest);
        public Task DeleteSubCategory(List<Product> products, int subCategoryId);
    }
}
