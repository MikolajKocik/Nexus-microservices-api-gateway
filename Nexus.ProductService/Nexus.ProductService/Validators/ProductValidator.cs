using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nexus.ProductService.Features.Products;

namespace Nexus.ProductService.Validators;

public class ProductValidator : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(p => p.Id);
        builder.ToTable("Products");

        builder.Property(p => p.Id).ValueGeneratedOnAdd();
        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(p => p.Price)  
            .HasColumnType("decimal(18,2)");
    }
}
