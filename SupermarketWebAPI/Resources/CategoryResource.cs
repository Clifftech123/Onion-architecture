namespace SupermarketWebAPI.Resources
{
    public record CategoryResource
    {

       public required int Id { get; set; }
        public required string Name { get; set; }
    }
}
