using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace SupermarketWebAPI.Persistence.Context.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Property(p => p.Name).IsRequired().HasMaxLength(50);
            builder.Property(p => p.QuantityInPackage).IsRequired();
            builder.Property(p => p.UnitOfMeasurement).IsRequired();
        }
    }
}
