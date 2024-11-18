using INUPCO.Catalog.Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace INUPCO.Catalog.Infrastructure.Persistence.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.TradeCode)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasOne(x => x.Manufacturer)
            .WithMany(x => x.Products)
            .HasForeignKey(x => x.ManufacturerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Subsidiary)
            .WithMany(x => x.Products)
            .HasForeignKey(x => x.SubsidiaryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => x.TradeCode)
            .IsUnique();
    }
} 