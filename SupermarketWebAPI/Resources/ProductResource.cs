namespace SupermarketWebAPI.Resources
{
    public  record ProductResource
    {

        public required int Id { get; set; }
        public required string Name { get; set; }
        public required short QuantityInPackage { get; set; }
        public required string UnitOfMeasurement { get; set; }
        public required CategoryResource? Category { get; set; }
    }
}
