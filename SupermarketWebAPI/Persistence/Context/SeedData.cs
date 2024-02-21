using SupermarketWebAPI.Domain.Models;

namespace SupermarketWebAPI.Persistence.Context
{
    public static class SeedData
    {
        public static async Task Seed(AppDbContext context)
        {
            var products = new List<Product>
            {
                new() {
                    Id = 100,
                    Name = "Apple",
                    QuantityInPackage = 1,
                    UnitOfMeasurement = UnitOfMeasurement.Unity,
                    CategoryId = 200
                },
                new() {
                    Id = 101,
                    Name = "Milk",
                    QuantityInPackage = 2,
                    UnitOfMeasurement = UnitOfMeasurement.Liter,
                    CategoryId = 500,
                }
            };

            var categories = new List<Category>
            {
                new() { Id = 100, Name = "Fruits and Vegetables" },
            };

            context.Products.AddRange(products);
            context.Categories.AddRange(categories);

            await context.SaveChangesAsync();
        }
    }
}
