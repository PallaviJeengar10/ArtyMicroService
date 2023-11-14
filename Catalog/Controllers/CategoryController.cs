using Arty.Dtos;
using Catalog.Services.Categories;
using Catalog.Services.SubCategories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedModels.Models;

namespace CatalogMicroservice.Controllers
{
    [Route("category")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly ISubCategoryService _subCategoryService;

        public CategoryController(ICategoryService categoryService, ISubCategoryService subCategoryService)
        {
            _categoryService = categoryService;
            _subCategoryService = subCategoryService;
        }

        [Route("getCategoryList")]
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetCategory()
        {
            List<Category> categories = await _categoryService.GetCategories();
            return Ok(categories);
        }

        [Route("addNewCategory")]
        [HttpPost]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> CreateCategory(CategoryDto newCategory)
        {
            if (newCategory == null)
            {
                return BadRequest();
            }
            int categoryId = await _categoryService.AddCategory(newCategory);
            return CreatedAtAction(nameof(GetCategory), new { id = categoryId });
        }

        [Route("updateCategory/{id}")]
        [HttpPut]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> UpdateCategory([FromRoute] int id, CategoryDto updatecategoryRequest)
        {
            if (id == 0)
            {
                return await Task.FromResult<IActionResult>(NotFound());
            }
            if(await _categoryService.UpdateCategory(id, updatecategoryRequest))
            {
                return Ok("Category Updated");
            }
            return NotFound();
        }

        [Route("deleteCategory/{categoryId}")]
        [HttpDelete]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> DeleteCategory([FromRoute] int categoryId)
        {
            try
            {
                List<SubCategory> subCategories = await _subCategoryService.GetSubCategoriesByCategoryId(categoryId);
                await _categoryService.DeleteCategory(subCategories, categoryId);
                return Ok("Removed Category");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
