
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Reflection;
using SupermarketWebAPI.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace SupermarketWebAPI.Persistence.Context
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }

}
