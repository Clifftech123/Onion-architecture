namespace SupermarketWebAPI.Domain.Models
{
    public class Category
    {

        public int id { get; set; }
        public string name { get; set; } = null!;
        public List<Product> Products { get; set; } = new();

    }
}
