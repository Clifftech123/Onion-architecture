namespace SupermarketWebAPI.Resources
{
    public record QueryResource
    {

        public required int page { get; set; }
        public required int ItemsPerPage { get; init; }
    }
}
