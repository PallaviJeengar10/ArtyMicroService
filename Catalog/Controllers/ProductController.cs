using Arty.Dtos;
using Catalog.Services.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedModels.Models;

namespace CatalogMicroservice.Controllers
{
    [Route("product")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [Route("getProductList")]
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetProducts()
        {
            List<Product> products = await _productService.GetProducts();
            return Ok(products);
        }

        [Route("addNewProduct")]
        [HttpPost]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> AddProduct(ProductsDto productInfo)
        {
            int productId = await _productService.AddProduct(productInfo);
            return CreatedAtAction(nameof(GetProducts), new { id = productId });
        }

        [Route("updateProduct/{id}")]
        [HttpPut]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> UpdateProduct([FromRoute] int id, ProductsDto productInfo)
        {
            if (id == 0)
            {
                return await Task.FromResult<IActionResult>(NotFound());
            }
            if (await _productService.UpdateProduct(id, productInfo))
            {
                return Ok();
            }
            return NotFound();
        }

        [Route("deleteProduct/{productId}")]
        [HttpDelete]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> DeleteProduct([FromRoute] int productId)
        {
            try
            {
                await _productService.DeleteProduct(productId);

                return Ok("Removed Category");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
