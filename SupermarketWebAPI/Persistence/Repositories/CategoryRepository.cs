using Microsoft.EntityFrameworkCore;
using SupermarketWebAPI.Domain.Models;
using SupermarketWebAPI.Domain.Repositories;
using SupermarketWebAPI.Persistence.Context;

namespace SupermarketWebAPI.Persistence.Repositories
{
    public class CategoryRepository(AppDbContext context) : BaseRepository(context), ICategoryRepository
    {

        
        public async Task<IEnumerable<Category>> ListAsync()
            => await _context.Categories.AsNoTracking().ToListAsync();

        public async Task AddAsync(Category category)
            => await _context.Categories.AddAsync(category);

        public async Task<Category?> FindByIdAsync(int id)
            => await _context.Categories.FindAsync(id);

        public void Update(Category category)
        {
            _context.Categories.Update(category);
        }

        public void Remove(Category category)
        {
            _context.Categories.Remove(category);
        }
    }

}
