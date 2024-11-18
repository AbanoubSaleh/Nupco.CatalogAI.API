using INUPCO.Catalog.Domain.Entities.GenericItemPharmas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace INUPCO.Catalog.Infrastructure.Persistence.Configurations;

public class GenericItemPharmaConfiguration : IEntityTypeConfiguration<GenericItemPharma>
{
    public void Configure(EntityTypeBuilder<GenericItemPharma> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Code).HasMaxLength(50).IsRequired();
        builder.Property(x => x.Name).HasMaxLength(200).IsRequired();
        builder.Property(x => x.Description).HasMaxLength(500);
        builder.Property(x => x.CustomerCode).HasMaxLength(50);
        builder.Property(x => x.Status).HasConversion<string>();
        
        builder.HasMany(x => x.Comments)
            .WithOne()
            .HasForeignKey("GenericItemPharmaId")
            .OnDelete(DeleteBehavior.Cascade);
    }
} 