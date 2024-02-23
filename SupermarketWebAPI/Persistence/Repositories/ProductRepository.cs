using Microsoft.EntityFrameworkCore;
using SupermarketWebAPI.Domain.Models.Queries;
using SupermarketWebAPI.Domain.Repositories;
using SupermarketWebAPI.Persistence.Context;

namespace SupermarketWebAPI.Persistence.Repositories
{
    public class ProductRepository(AppDbContext context) : BaseRepository(context), IProductRepository
    {
        public async Task<QueryResult<Product>> ListAsync(ProductsQuery query)
        {
            IQueryable<Product> queryable = _context.Products
                                                    .Include(p => p.Category)
                                                    .AsNoTracking();

          
            if (query.CategoryId.HasValue && query.CategoryId > 0)
            {
                queryable = queryable.Where(p => p.CategoryId == query.CategoryId);
            }

           
            int totalItems = await queryable.CountAsync();

          
            List<Product> products = await queryable.Skip((query.Page - 1) * query.ItemsPerPage)
              .Take(query.ItemsPerPage)
              .ToListAsync();

          
            return new QueryResult<Product>
            {
                Items = products,
                TotalItems = totalItems,
            };
        }

        public async Task<Product?> FindByIdAsync(int id)
            => await _context.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id); 

        public async Task AddAsync(Product product)
            => await _context.Products.AddAsync(product);

        public void Update(Product product)
        {
            _context.Products.Update(product);
        }

        public void Remove(Product product)
        {
            _context.Products.Remove(product);
        }
    }
}
