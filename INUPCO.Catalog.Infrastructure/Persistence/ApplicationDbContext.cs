using Microsoft.EntityFrameworkCore;
using INUPCO.Catalog.Domain.Entities.GenericItemPharmas;
using INUPCO.Catalog.Domain.Entities.Customers;
using INUPCO.Catalog.Domain.Entities.Manufacturers;
using INUPCO.Catalog.Domain.Entities.Products;
using INUPCO.Catalog.Domain.Entities.Subsidiaries;
using System.Reflection;
using INUPCO.Catalog.Infrastructure.Persistence.Extensions;
using EFCore.BulkExtensions;

namespace INUPCO.Catalog.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        // Enable bulk operations logging in development
        if (Database.ProviderName == "Microsoft.EntityFrameworkCore.SqlServer")
        {
            Database.SetCommandTimeout(TimeSpan.FromMinutes(3));
        }
    }

    public DbSet<GenericItemPharma> GenericItemPharmas => Set<GenericItemPharma>();
    public DbSet<Comment> Comments => Set<Comment>();
    public DbSet<CustomerGenericItemPharmaCodeMapping> CustomerGenericItemPharmaCodeMappings => Set<CustomerGenericItemPharmaCodeMapping>();
    public DbSet<Manufacturer> Manufacturers => Set<Manufacturer>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Subsidiary> Subsidiaries => Set<Subsidiary>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        modelBuilder.SeedData();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.UseSqlServer(options => 
            {
                options.EnableRetryOnFailure(3);
                options.CommandTimeout(180); // 3 minutes for bulk operations
            });
        }
    }
} 