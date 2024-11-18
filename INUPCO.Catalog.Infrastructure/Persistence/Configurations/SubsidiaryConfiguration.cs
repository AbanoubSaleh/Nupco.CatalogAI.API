using INUPCO.Catalog.Domain.Entities.Subsidiaries;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace INUPCO.Catalog.Infrastructure.Persistence.Configurations;

public class SubsidiaryConfiguration : IEntityTypeConfiguration<Subsidiary>
{
    public void Configure(EntityTypeBuilder<Subsidiary> builder)
    {
        builder.ToTable("Subsidiaries");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.Country)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasOne(x => x.Manufacturer)
            .WithMany(x => x.Subsidiaries)
            .HasForeignKey(x => x.ManufacturerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.Products)
            .WithOne(x => x.Subsidiary)
            .HasForeignKey(x => x.SubsidiaryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => new { x.Name, x.Country, x.ManufacturerId })
            .IsUnique();
    }
} 