using Arty.Dtos;
using Arty.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using SharedModels.Models;

namespace Catalog.DataRepositories.Products
{
    public class ProductRepository : IProductRepository
    {
        private readonly ArtyContext _context;
        public ProductRepository(ArtyContext context)
        {
            _context = context;
        }

        public async Task<int> AddProduct(Product product)
        {
            await _context.Products.AddAsync(product);
            _context.SaveChanges();
            return product.ProductId;
        }
        
        public async Task DeleteProduct(Product product)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }

        public async Task<Product> GetProductById(int productId)
        {
            var product =  await _context.Products.FindAsync(productId);
            return product ?? new Product();
        }

        public async Task<List<Product>> GetProductBySubCategoryId(int subCategoryId)
        {
            return await _context.Products
                    .Where(p => p.SubCategoryId == subCategoryId)
                    .ToListAsync();
        }

        public async Task<List<Product>> GetProducts()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task UpdateProduct(Product product, ProductsDto productInfo)
        {
            product.ProductDescription = productInfo.ProductDescription;
            product.ProductName = productInfo.ProductName;
            product.Price = productInfo.Price;
            await _context.SaveChangesAsync();
        }
    }
}
