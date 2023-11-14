using Arty.Dtos;
using Catalog.Services.Products;
using Catalog.Services.SubCategories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedModels.Models;

namespace CatalogMicroservice.Controllers
{
    [Route("subCategory")]
    public class SubCategoryController : Controller
    {
        private readonly ISubCategoryService _subCategoryService;
        private readonly IProductService _productService;

        public SubCategoryController(ISubCategoryService subCategoryService, IProductService productService)
        {
            _subCategoryService = subCategoryService;
            _productService = productService;
        }

        [Route("getSubCategoryList")]
        [HttpGet]
        [Authorize]

        public async Task<IActionResult> GetSubCategory()
        {
            List<SubCategory> subCategories = await _subCategoryService.GetSubCategories();
            return Ok(subCategories);
        }

        [Route("addNewSubCategory")]
        [HttpPost]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> CreateSubCategory(SubCategoryDto newCategory)
        {
            if (newCategory == null)
            {
                return BadRequest();
            }
            int subCategoryId = await _subCategoryService.AddSubCategory(newCategory);
            return CreatedAtAction(nameof(GetSubCategory), new { id = subCategoryId });
        }

        [Route("updateSubCategory/{id}")]
        [HttpPut]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> UpdateSubCategory([FromRoute] int id, SubCategory updatecategoryRequest)
        {
            if (id == 0)
            {
                return await Task.FromResult<IActionResult>(NotFound());
            }

            if (await _subCategoryService.UpdateSubCategory(id, updatecategoryRequest))
            {
                return Ok("Sub Category Updated");
            }
            return NotFound();
        }

        [Route("deleteSubCategory/{subCategoryId}")]
        [HttpDelete]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> DeleteSubCategory([FromRoute] int subCategoryId)
        {
            try
            {
                List<Product> product = await _productService.GetProductsBySubCategoryId(subCategoryId);
                await _subCategoryService.DeleteSubCategory(product, subCategoryId);

                return Ok("Removed Product");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
