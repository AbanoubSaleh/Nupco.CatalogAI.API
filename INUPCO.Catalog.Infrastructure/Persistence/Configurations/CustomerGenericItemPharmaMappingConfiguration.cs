using INUPCO.Catalog.Domain.Entities.Customers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace INUPCO.Catalog.Infrastructure.Persistence.Configurations;

public class CustomerGenericItemPharmaMappingConfiguration : IEntityTypeConfiguration<CustomerGenericItemPharmaCodeMapping>
{
    public void Configure(EntityTypeBuilder<CustomerGenericItemPharmaCodeMapping> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.CustomerCode)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.CustomerSpecificCode)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasOne(x => x.GenericItemPharma)
            .WithMany(x => x.CustomerMappings)
            .HasForeignKey(x => x.GenericItemPharmaId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => new { x.CustomerCode, x.CustomerSpecificCode })
            .IsUnique();
    }
} 