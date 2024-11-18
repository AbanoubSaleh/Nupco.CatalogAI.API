using INUPCO.Catalog.Domain.Entities.Manufacturers;
using INUPCO.Catalog.Domain.Entities.Products;
using INUPCO.Catalog.Domain.Entities.Subsidiaries;
using Microsoft.EntityFrameworkCore;

namespace INUPCO.Catalog.Infrastructure.Persistence.Extensions;

public static class ModelBuilderExtensions
{
    public static void SeedData(this ModelBuilder builder)
    {
        // Seed Manufacturers
        builder.Entity<Manufacturer>().HasData(
            new
            {
                Id = 1,
                Name = "Pfizer",
                Country = "USA",
                CreatedDate = DateTime.UtcNow,
                CreatedBy = (string?)null,
                LastModifiedDate = (DateTime?)null,
                LastModifiedBy = (string?)null
            },
            new
            {
                Id = 2,
                Name = "Novartis",
                Country = "Switzerland",
                CreatedDate = DateTime.UtcNow,
                CreatedBy = (string?)null,
                LastModifiedDate = (DateTime?)null,
                LastModifiedBy = (string?)null
            }
        );

        // Seed Subsidiaries
        builder.Entity<Subsidiary>().HasData(
            new
            {
                Id = 1,
                Name = "Pfizer Germany GmbH",
                Country = "Germany",
                ManufacturerId = 1,
                CreatedDate = DateTime.UtcNow,
                CreatedBy = (string?)null,
                LastModifiedDate = (DateTime?)null,
                LastModifiedBy = (string?)null
            },
            new
            {
                Id = 2,
                Name = "Novartis Spain SA",
                Country = "Spain",
                ManufacturerId = 2,
                CreatedDate = DateTime.UtcNow,
                CreatedBy = (string?)null,
                LastModifiedDate = (DateTime?)null,
                LastModifiedBy = (string?)null
            }
        );

        // Seed Products
        builder.Entity<Product>().HasData(
            new
            {
                Id = 1,
                Name = "Lipitor",
                TradeCode = "PFE001",
                ManufacturerId = 1, // Pfizer
                SubsidiaryId = 1, // Pfizer Germany
                CreatedDate = DateTime.UtcNow,
                CreatedBy = (string?)null,
                LastModifiedDate = (DateTime?)null,
                LastModifiedBy = (string?)null
            },
            new
            {
                Id = 2,
                Name = "Xarelto",
                TradeCode = "PFE002",
                ManufacturerId = 1, // Pfizer
                SubsidiaryId = (int?)null,
                CreatedDate = DateTime.UtcNow,
                CreatedBy = (string?)null,
                LastModifiedDate = (DateTime?)null,
                LastModifiedBy = (string?)null
            },
            new
            {
                Id = 3,
                Name = "Entresto",
                TradeCode = "NOV001",
                ManufacturerId = 2, // Novartis
                SubsidiaryId = 2, // Novartis Spain
                CreatedDate = DateTime.UtcNow,
                CreatedBy = (string?)null,
                LastModifiedDate = (DateTime?)null,
                LastModifiedBy = (string?)null
            }
        );
    }
} 