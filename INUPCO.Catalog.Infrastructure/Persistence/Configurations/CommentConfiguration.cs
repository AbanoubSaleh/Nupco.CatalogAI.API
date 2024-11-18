using INUPCO.Catalog.Domain.Entities.GenericItemPharmas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace INUPCO.Catalog.Infrastructure.Persistence.Configurations;

public class CommentConfiguration : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Content).HasMaxLength(1000).IsRequired();
        builder.Property(x => x.UserId).HasMaxLength(50).IsRequired();
        builder.Property(x => x.Type).HasConversion<string>();
        builder.Property(x => x.CreatedDate).IsRequired();
    }
} 