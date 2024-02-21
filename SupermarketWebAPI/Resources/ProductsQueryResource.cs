namespace SupermarketWebAPI.Resources
{
    public record ProductsQueryResource : QueryResource
    {
        public int ? CategoryId { get; set; }
    }
}
