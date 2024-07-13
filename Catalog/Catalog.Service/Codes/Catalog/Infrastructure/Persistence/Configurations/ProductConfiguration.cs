using eShop.Catalog.Domain.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eShop.Catalog.Infrastructure.Persistence.Configurations;

//? Mapping (Domain Class -> Database)
public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");

        builder.HasKey(p => p.Id); //? Primary Key

        builder.Property(p => p.Id).ValueGeneratedNever();
        builder.Property(p => p.Price).IsRequired().HasPrecision(7, 2);
        builder.Property(p => p.Name).IsRequired().IsUnicode().HasMaxLength(100);
        builder.Property(p => p.Description).IsRequired(false).IsUnicode().HasMaxLength(250);
    }
}
