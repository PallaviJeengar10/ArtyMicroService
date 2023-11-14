using Arty.Dtos;
using AutoMapper;
using Catalog.DataRepositories.SubCategories;
using SharedModels.Models;

namespace Catalog.Services.SubCategories
{
    public class SubCategoryService : ISubCategoryService
    {
        private readonly ISubCategoryRepository _repository;
        private readonly IMapper _mapper;

        public SubCategoryService(ISubCategoryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<int> AddSubCategory(SubCategoryDto newSubCategory)
        {
            SubCategory subCategory = _mapper.Map<SubCategory>(newSubCategory);
            return await _repository.AddSubCategory(subCategory);
        }

        public async Task DeleteSubCategory(List<Product> products, int subCategoryId)
        {
            SubCategory subCategory = await _repository.GetSubCategoryById(subCategoryId);
            await _repository.DeleteSubCategory(products, subCategory);
        }

        public async Task<List<SubCategory>> GetSubCategories()
        {
            return await _repository.GetSubCategories();
        }

        public async Task<List<SubCategory>> GetSubCategoriesByCategoryId(int categoryId)
        {
            return await _repository.GetSubCategoriesByCategoryId(categoryId);
        }

        public async Task<bool> UpdateSubCategory(int subCategoryId, SubCategory updateSubCategoryRequest)
        {
            SubCategory subCategory = await _repository.GetSubCategoryById(subCategoryId);
            if (subCategory != null)
            {
                await _repository.UpdateSubCategory(subCategory, updateSubCategoryRequest);
                return true;
            }
            return false;
        }
    }
}
