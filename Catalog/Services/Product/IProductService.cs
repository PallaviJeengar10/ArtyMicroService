using Arty.Dtos;
using SharedModels.Models;

namespace Catalog.Services.Products
{
    public interface IProductService
    {
        public Task<List<Product>> GetProductsBySubCategoryId(int subCategoryId);
        public Task<List<Product>> GetProducts();
        public Task<int> AddProduct(ProductsDto product);
        public Task<Boolean> UpdateProduct(int productId, ProductsDto product);
        public Task DeleteProduct(int productId);
    }
}
