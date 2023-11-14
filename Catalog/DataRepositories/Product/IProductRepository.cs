using Arty.Dtos;
using SharedModels.Models;
namespace Catalog.DataRepositories.Products
{
    public interface IProductRepository
    {
        public Task<List<Product>> GetProductBySubCategoryId(int subCategoryId);
        public Task<List<Product>> GetProducts();
        public Task<int> AddProduct(Product product);
        public Task UpdateProduct(Product product, ProductsDto productInfo);
        public Task DeleteProduct(Product product);
        public Task<Product> GetProductById(int productId);
    }
}
