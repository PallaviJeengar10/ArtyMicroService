using Arty.Dtos;
using AutoMapper;
using Catalog.DataRepositories.Products;
using SharedModels.Models;

namespace Catalog.Services.Products
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;
        public ProductService(IProductRepository productRepository, IMapper mapper) {
            _repository = productRepository;
            _mapper = mapper;
        }

        public async Task<int> AddProduct(ProductsDto productInfo)
        {
            Product product = _mapper.Map<Product>(productInfo);
            return await _repository.AddProduct(product);  
        }

        public async Task DeleteProduct(int productId)
        {
            Product product = await _repository.GetProductById(productId);
            await _repository.DeleteProduct(product);
        }

        public Task<List<Product>> GetProducts()
        {
            return _repository.GetProducts();
        }

        public async Task<List<Product>> GetProductsBySubCategoryId(int subCategoryId)
        {
            return await _repository.GetProductBySubCategoryId(subCategoryId);
        }

        public async Task<bool> UpdateProduct(int productId, ProductsDto productInfo)
        {
            Product product = await _repository.GetProductById(productId);
            if (product != null)
            {
                await _repository.UpdateProduct(product, productInfo);
                return true;
            }
            return false;
        }
    }
}
