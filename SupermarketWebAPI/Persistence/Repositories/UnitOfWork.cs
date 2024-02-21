using SupermarketWebAPI.Domain.Repositories;
using SupermarketWebAPI.Persistence.Context;

namespace SupermarketWebAPI.Persistence.Repositories
{
    public class UnitOfWork(AppDbContext context) : IUnitOfWork
    {
        private readonly AppDbContext _context = context;

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
