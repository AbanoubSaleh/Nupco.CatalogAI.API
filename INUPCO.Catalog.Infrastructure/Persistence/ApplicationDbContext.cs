using Microsoft.EntityFrameworkCore;
using INUPCO.Catalog.Domain.Entities.GenericItemPharmas;
using INUPCO.Catalog.Domain.Entities.Customers;

namespace INUPCO.Catalog.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<GenericItemPharma> GenericItemPharmas => Set<GenericItemPharma>();
    public DbSet<Comment> Comments => Set<Comment>();
    public DbSet<CustomerGenericItemPharmaCodeMapping> CustomerGenericItemPharmaCodeMappings => Set<CustomerGenericItemPharmaCodeMapping>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
} 