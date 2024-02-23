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

        // Constructor with dependency injection
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

        // Method to list products with caching
        public async Task<QueryResult<Product>> ListAsync(ProductsQuery query)
        {
            // Generate cache key
            string cacheKey = GetCacheKeyForProductsQuery(query);

            // Get or create cache entry
            var products = await _cache.GetOrCreateAsync(cacheKey, (entry) =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1);
                return _productRepository.ListAsync(query);
            });

            // Return products, assuming they are not null
            return products ?? new QueryResult<Product>();
        }

        // Method to save a product
        public async Task<Response<Product>> SaveAsync(Product product)
        {
            try
            {
                // Check if category exists
                var existingCategory = await _categoryRepository.FindByIdAsync(product.CategoryId);
                if (existingCategory == null)
                    return new Response<Product>("Invalid category.");

                // Add product and save changes
                await _productRepository.AddAsync(product);
                await _unitOfWork.CompleteAsync();

                return new Response<Product>(product);
            }
            catch (Exception ex)
            {
                // Log error and return response with error message
                _logger.LogError(ex, "Could not save product.");
                return new Response<Product>($"An error occurred when saving the product: {ex.Message}");
            }
        }



        // method to save post to draft 

        public async Task<Response<Product>> SaveDraftAsync(Product product)
        {
            try
            {
                // Set the product as a draft
                product.IsDraft = true;

                // Add product and save changes
                await _productRepository.AddAsync(product);
                await _unitOfWork.CompleteAsync();

                return new Response<Product>(product);
            }
            catch (Exception ex)
            {
                // Log error and return response with error message
                _logger.LogError(ex, "Could not save draft product.");
                return new Response<Product>($"An error occurred when saving the draft product: {ex.Message}");
            }
        }



        // Method to update a product
        public async Task<Response<Product>> UpdateAsync(int id, Product product)
        {
            // Check if product exists
            var existingProduct = await _productRepository.FindByIdAsync(id);
            if (existingProduct == null)
                return new Response<Product>("Product not found.");

            // Check if category exists
            var existingCategory = await _categoryRepository.FindByIdAsync(product.CategoryId);
            if (existingCategory == null)
                return new Response<Product>("Invalid category.");

            // Update product details
            existingProduct.Name = product.Name;
            existingProduct.UnitOfMeasurement = product.UnitOfMeasurement;
            existingProduct.QuantityInPackage = product.QuantityInPackage;
            existingProduct.CategoryId = product.CategoryId;

            try
            {
                // Update product and save changes
                _productRepository.Update(existingProduct);
                await _unitOfWork.CompleteAsync();

                return new Response<Product>(existingProduct);
            }
            catch (Exception ex)
            {
                // Log error and return response with error message
                _logger.LogError(ex, "Could not update product with ID {id}.", id);
                return new Response<Product>($"An error occurred when updating the product: {ex.Message}");
            }
        }

        // Method to delete a product
        public async Task<Response<Product>> DeleteAsync(int id)
        {
            // Check if product exists
            var existingProduct = await _productRepository.FindByIdAsync(id);
            if (existingProduct == null)
                return new Response<Product>("Product not found.");

            try
            {
                // Remove product and save changes
                _productRepository.Remove(existingProduct);
                await _unitOfWork.CompleteAsync();

                return new Response<Product>(existingProduct);
            }
            catch (Exception ex)
            {
                // Log error and return response with error message
                _logger.LogError(ex, "Could not delete product with ID {id}.", id);
                return new Response<Product>($"An error occurred when deleting the product: {ex.Message}");
            }
        }

        // Method to generate cache key
        private static string GetCacheKeyForProductsQuery(ProductsQuery query)
            => $"{CacheKeys.ProductsList}_{query.CategoryId}_{query.Page}_{query.ItemsPerPage}";
    }
}
