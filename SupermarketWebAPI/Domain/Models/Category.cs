namespace SupermarketWebAPI.Domain.Models
{
    public class Category
    {
        internal string Name;

        public int id { get; set; }
        public int Id { get; internal set; }
        public string name { get; set; } = null!;
        public List<Product> Products { get; set; } = new();

    }
}
