using Microsoft.Extensions.Caching.Memory;
using SupermarketWebAPI.Domain.Models.Queries;
using SupermarketWebAPI.Domain.Repositories;
using SupermarketWebAPI.Domain.Services;
using SupermarketWebAPI.Infrastructure;

namespace SupermarketWebAPI.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMemoryCache _cache;
        private readonly ILogger<ProductService> _logger;

        public ProductService
        (
            IProductRepository productRepository,
            ICategoryRepository categoryRepository,
            IUnitOfWork unitOfWork,
            IMemoryCache cache,
            ILogger<ProductService> logger
        )
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
            _cache = cache;
            _logger = logger;
        }

        public async Task<QueryResult<Product>> ListAsync(ProductsQuery query)
        {
           
            string cacheKey = GetCacheKeyForProductsQuery(query);

            var products = await _cache.GetOrCreateAsync(cacheKey, (entry) =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1);
                return _productRepository.ListAsync(query);
            });

            return products!;
        }

        public async Task<Response<Product>> SaveAsync(Product product)
        {
            try
            {
              
                var existingCategory = await _categoryRepository.FindByIdAsync(product.CategoryId);
                if (existingCategory == null)
                    return new Response<Product>("Invalid category.");

                await _productRepository.AddAsync(product);
                await _unitOfWork.CompleteAsync();

                return new Response<Product>(product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Could not save product.");
                return new Response<Product>($"An error occurred when saving the product: {ex.Message}");
            }
        }

        public async Task<Response<Product>> UpdateAsync(int id, Product product)
        {
            var existingProduct = await _productRepository.FindByIdAsync(id);

            if (existingProduct == null)
                return new Response<Product>("Product not found.");

            var existingCategory = await _categoryRepository.FindByIdAsync(product.CategoryId);
            if (existingCategory == null)
                return new Response<Product>("Invalid category.");

            existingProduct.Name = product.Name;
            existingProduct.UnitOfMeasurement = product.UnitOfMeasurement;
            existingProduct.QuantityInPackage = product.QuantityInPackage;
            existingProduct.CategoryId = product.CategoryId;

            try
            {
                _productRepository.Update(existingProduct);
                await _unitOfWork.CompleteAsync();

                return new Response<Product>(existingProduct);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Could not update product with ID {id}.", id);
                return new Response<Product>($"An error occurred when updating the product: {ex.Message}");
            }
        }

        public async Task<Response<Product>> DeleteAsync(int id)
        {
            var existingProduct = await _productRepository.FindByIdAsync(id);

            if (existingProduct == null)
                return new Response<Product>("Product not found.");

            try
            {
                _productRepository.Remove(existingProduct);
                await _unitOfWork.CompleteAsync();

                return new Response<Product>(existingProduct);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Could not delete product with ID {id}.", id);
                return new Response<Product>($"An error occurred when deleting the product: {ex.Message}");
            }
        }

        private static string GetCacheKeyForProductsQuery(ProductsQuery query)
            => $"{CacheKeys.ProductsList}_{query.CategoryId}_{query.Page}_{query.ItemsPerPage}";
    }
}
