using SupermarketWebAPI.Domain.Models.Queries;

namespace SupermarketWebAPI.Domain.Services
{
    public interface IProductService
    {
        Task<QueryResult<Product>> ListAsync(ProductsQuery query);
        Task<Response<Product>> SaveAsync(Product product);
        Task<Response<Product>> UpdateAsync(int id, Product product);
        Task<Response<Product>> DeleteAsync(int id);
        Task<Response<Product>> SaveDraftAsync(Product product); 
    }

}
