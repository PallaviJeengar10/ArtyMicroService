using SharedModels.Models;

namespace Catalog.DataRepositories.SubCategories
{
    public interface ISubCategoryRepository
    {
        public Task<List<SubCategory>> GetSubCategoriesByCategoryId(int categoryId);
        public Task<List<SubCategory>> GetSubCategories();
        public Task<int> AddSubCategory(SubCategory subCategory);
        public Task UpdateSubCategory(SubCategory subCategory, SubCategory updateSubCategoryRequest);
        public Task DeleteSubCategory(List<Product> products, SubCategory subCategory);
        public Task<SubCategory> GetSubCategoryById(int subCategoryId);
    }
}
