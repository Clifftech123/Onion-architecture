namespace SupermarketWebAPI.Domain.Models.Queries
{
    public class QueryResult
    {
        public List<T> Items { get; set; } = new List<T>();
        public int TotalItems { get; set; } = 0;
    }
}
