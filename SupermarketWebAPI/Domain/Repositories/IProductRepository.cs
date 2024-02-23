

using SupermarketWebAPI.Domain.Models.Queries;

namespace SupermarketWebAPI.Domain.Repositories
{
    public interface IProductRepository
    {
        Task<QueryResult<Product>> ListAsync(ProductsQuery query);
        Task AddAsync(Product product);
        Task<Product?> FindByIdAsync(int id);
        void Update(Product product);
        void Remove(Product product);
    }
}
