namespace SupermarketWebAPI.Resources
{
    public record ProductsQueryResource : QueryResource
    {
        public int ? CategoryId { get; set; }
        public object Page { get; internal set; }
    }
}
